using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Image inventoryImage;
    [SerializeField] private TMP_Text itemCounter;
    public List<AreaManager> areas;
    [HideInInspector] public AreaManager currentArea;
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
    
    public void Collect(Collectible collectible)
    {
        currentArea.collectiblesCollected++;
        if(currentArea.collectiblesCollected == 1)
        {
            //Add new inventory image
            inventoryImage.sprite = collectible.inventoryIcon;            
        }
        else if(currentArea.collectiblesCollected > 1)
        {
            itemCounter.text = string.Empty + currentArea.collectiblesCollected;
        }
    }

    private void UseCollectibles()
    {
        if(!currentArea.lightOn)
        {
            currentArea.collectiblesCollected = 0;
            currentArea.collectiblesNeeded = 0;
            SwitchLight(true);
        }        
    }

    public void TryUseGenerator(Generator generator)
    {
        if(generator.correspondingRoom == currentArea.correspondingRoom)
        {
            if(currentArea.collectiblesCollected >= currentArea.collectiblesNeeded)
            {
                UseCollectibles();
            }
        }
        else
        {
            Debug.LogError("Something went wrong. The room of the activated generator does not match the room of the current area.");
        }
    }
}
