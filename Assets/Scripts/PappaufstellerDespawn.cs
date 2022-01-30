using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PappaufstellerDespawn : MonoBehaviour
{

    public Room room;
    public Transform playerTransform;
    public int angleFrom = 128;
    public int angleTo = 132;
    public float maxDistance = 10;
    public float eventProbability = 0.1f;
    public int maxLivingTime = 5;

    private GameManager gameManager;
    private float livingSince;

    void Start()
    {
        gameManager = GameManager.gameManager;
        gameManager.LightSwitched += ToggleLight;
        livingSince = Time.realtimeSinceStartup;

        if(angleFrom > angleTo)
        {
            throw new UnityException("Winkel fehlkonfiguriert");
        }
    }

    private void OnDestroy()
    {
        gameManager.LightSwitched -= ToggleLight;
    }

    private void Update()
    {        
        MaybeDespawn();
    }

    private void MaybeDespawn()
    {
        float maybeRemove = Random.Range(0f, 1f);
        float angle = Vector3.Angle(playerTransform.forward, playerTransform.position - this.transform.position);
        float distance = Vector3.Distance(playerTransform.position, this.transform.position);

        // Debug.Log(angle + " "+ distance + " " + maybeRemove + "  " + (angle > angleFrom && angle < angleTo && maybeRemove == 1 && distance > maxDistance));
        if (angle > angleFrom && angle < angleTo && maybeRemove < eventProbability && distance > maxDistance)
        {
            Die();
        }

        float currentTime = Time.realtimeSinceStartup;

        if((livingSince + maxLivingTime < currentTime) && angle > 132)
        {
            Die();
        }
    }

    private void ToggleLight(bool lightOn, Room currentRoom)
    {
        if(room == currentRoom)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }
}
