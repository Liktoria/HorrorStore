using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyOswald : MonoBehaviour
{

    public GameObject friendlyOswald;
    public GameObject badOswald;
    public Room room;

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.gameManager;
        gameManager.LightSwitched += lightSwitch;

        AreaManager areaManager = gameManager.areas.Find(z => z.correspondingRoom == room);
        ChangeOswald(areaManager.lightOn);
    }

    private void OnDestroy()
    {
        gameManager.LightSwitched -= lightSwitch;
    }

    private void lightSwitch(bool light, Room room) 
    {
        if (gameManager.currentArea.correspondingRoom == room)
        {
            ChangeOswald(light);
        }
    }

    private void ChangeOswald(bool light)
    {
        if (light)
        {
            friendlyOswald.SetActive(true);
            badOswald.SetActive(false);
        }
        else
        {
            friendlyOswald.SetActive(false);
            badOswald.SetActive(true);
        }
    }

}
