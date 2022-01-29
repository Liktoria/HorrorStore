using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PappaufstellerSpawner : MonoBehaviour
{


    public float timebetween = 20;
    public GameObject pappaufsteller;
    public Transform spawnPoint;
    public bool lightIsOn = false;

    private float lastTime;

    private void Start()
    {
        lastTime = Time.realtimeSinceStartup;
    }

    private void FixedUpdate()
    {
        lightIsOn = GameManager.gameManager.currentArea.lightOn;
        intervallSpawn();
    }

    private void intervallSpawn()
    {
        float currentTime = Time.realtimeSinceStartup;
        if ((currentTime - timebetween) > lastTime && !lightIsOn)
        {
            Quaternion rotation = spawnPoint.parent.transform.rotation;
            rotation.y = rotation.y + 90;
            spawnPappaufsteller(pappaufsteller, spawnPoint.position, rotation);
            lastTime = currentTime;
        }
    }

    static void spawnPappaufsteller(GameObject prefab, Transform tPosition)
    {
        Vector3 position = tPosition.position;
        position.y = 0.5f;
        Quaternion rotation = tPosition.rotation;
        spawnPappaufsteller(prefab, position, rotation);
    }

    static void spawnPappaufsteller(GameObject prefab, Vector3 tPosition, Quaternion tRotation)
    {
        Debug.Log("Spawn");
        Vector3 position = tPosition;
        position.y = 0.5f;
        Quaternion rotation = tRotation;
        Instantiate(prefab, position, rotation);
    }
}
