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

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.gameManager;
        gameManager.LightSwitched += ToggleLight;

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
        int maybeRemove = Random.Range(0, 2);
        float angle = Vector3.Angle(playerTransform.forward, playerTransform.position - this.transform.position);
        float distance = Vector3.Distance(playerTransform.position, this.transform.position);

        // Debug.Log(angle + " "+ distance + " " + maybeRemove + "  " + (angle > angleFrom && angle < angleTo && maybeRemove == 1 && distance > maxDistance));
        if (angle > angleFrom && angle < angleTo && maybeRemove == 1 && distance > maxDistance)
        {
            Die();
        }
    }

    private void ToggleLight(bool lightOn)
    {
        if(room == gameManager.currentArea.correspondingRoom)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }
}
