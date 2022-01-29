using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<AreaManager> areas;
    public AreaManager currentArea;
    public event Action<bool> LightSwitched;
    public int collectedItemsRoom1;
    public int neededItemsRoom1;
    public int collectedItemsRoom2;
    public int neededItemsRoom2;
    public int collectedItemsRoom3;
    public int neededItemsRoom3;

    public static GameManager gameManager;

    private void Awake()
    {
        if(gameManager == null)
        {
            gameManager = this;
        }

        if (areas.Count > 0)
        {
            currentArea = areas[0];
        }
        else
        {
            throw new UnityException("Es muss mindestens eine Area im Gamemanager konfiguriert sein.");
        }
    }  
    
    public void SwitchLight(bool lightOn)
    {
        currentArea.lightOn = lightOn;
        LightSwitched?.Invoke(lightOn);
    }   

    public void UseCollectibles()
    {
        int roomNumber = areas.IndexOf(currentArea);
        switch(roomNumber)
        {
            case 0:
                Debug.LogWarning("Using collectibles in the start area wasn't planned.");
                return;                
            case 1:
                collectedItemsRoom1 = 0;
                neededItemsRoom1 = 0;
                break;
            case 2:
                collectedItemsRoom2 = 0;
                neededItemsRoom2 = 0;
                break;
            case 3:
                collectedItemsRoom3 = 0;
                neededItemsRoom3 = 0;
                break;
        }
        SwitchLight(true);
    }
}
