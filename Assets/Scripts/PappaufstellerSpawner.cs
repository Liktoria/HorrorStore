using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PappaufstellerSpawner : MonoBehaviour
{


    public float timeToSpawn = 20;
    public GameObject pappaufsteller;
    public Transform spawnPoint;
    public bool lightIsOn = false;
    private GameManager gameManager;

    private float lastTime;

    private void Start()
    {
        lastTime = Time.realtimeSinceStartup;
        gameManager = GameManager.gameManager;
        gameManager.LightSwitched += ToggleLight;
    }

    private void OnDestroy()
    {
        gameManager.LightSwitched -= ToggleLight;
    }

    private void Update()
    {
            intervallSpawn();
    }

    private void intervallSpawn()
    {
        float currentTime = Time.realtimeSinceStartup;
        if ((currentTime - timeToSpawn) > lastTime && !lightIsOn && DestinationOk(spawnPoint.position))
        {
            lastTime = currentTime;
            Quaternion rotation = spawnPoint.parent.transform.rotation;
            rotation.y = rotation.y + 90;
            GameObject pappaufstellerObject = spawnPappaufsteller(pappaufsteller, spawnPoint.position, rotation);

            PappaufstellerDespawn script = pappaufstellerObject.GetComponent<PappaufstellerDespawn>();
            script.room = gameManager.currentArea.correspondingRoom;
            script.playerTransform = spawnPoint.parent.transform;


        }
    }

    private bool DestinationOk(Vector3 position)
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(position, out hit, 1f, NavMesh.AllAreas))
        {
            return true;
        }
        return false;
    }

    static GameObject spawnPappaufsteller(GameObject prefab, Transform tPosition)
    {
        Vector3 position = tPosition.position;
        position.y = 0.5f;
        Quaternion rotation = tPosition.rotation;
        return spawnPappaufsteller(prefab, position, rotation);
    }

    static GameObject spawnPappaufsteller(GameObject prefab, Vector3 tPosition, Quaternion tRotation)
    {
        Debug.Log("Spawn");
        Vector3 position = tPosition;
        position.y = 0.5f;
        Quaternion rotation = tRotation;
        GameObject pappaufsteller = Instantiate(prefab, position, rotation);
        return pappaufsteller;
    }

    private void ToggleLight(bool lightOn, Room currentRoom)
    {
        lightIsOn = lightOn;
    }
}
