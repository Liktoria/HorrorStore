using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emitter3D : MonoBehaviour
{
    [SerializeField] private Room room;
    [SerializeField] private FMODUnity.StudioEventEmitter eventEmitter;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.gameManager.LightSwitched += LightSwitched;
    }

    private void LightSwitched(bool lightOn, Room currentRoom)
    {
        if(currentRoom == room && lightOn)
        {
            eventEmitter.Stop();
        }
        else if(currentRoom == room && !lightOn)
        {
            eventEmitter.Play();
        }
    }
}
