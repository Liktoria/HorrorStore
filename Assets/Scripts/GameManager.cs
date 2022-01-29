using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<AreaManager> areas;
    [HideInInspector] public AreaManager currentArea;
    public event Action<bool, Room> LightSwitched;
    public int collectedItemsRoom1;
    public int neededItemsRoom1;
    public int collectedItemsRoom2;
    public int neededItemsRoom2;
    public int collectedItemsRoom3;
    public int neededItemsRoom3;

    public static GameManager gameManager;

    private void Awake()
    {
        if (gameManager == null)
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
        Debug.Log("Setting light to " + lightOn + " in area " + currentArea.correspondingRoom);
        LightSwitched?.Invoke(lightOn, currentArea.correspondingRoom);
    }

    public void Collect(Collectible collectible)
    {
        currentArea.collectiblesCollected++;
        if (currentArea.collectiblesCollected == 1)
        {
            currentArea.UIElement3D.SetActive(true);
            currentArea.itemAmountText.text = string.Empty;
        }
        else if (currentArea.collectiblesCollected > 1)
        {
            currentArea.itemAmountText.text = string.Empty + currentArea.collectiblesCollected;
        }
    }

    private void UseCollectibles(Generator generator)
    {
        if (!currentArea.lightOn)
        {
            if (currentArea.collectiblesCollected > 0)
            {
                generator.numberOfAddedParts += currentArea.collectiblesCollected;
                currentArea.collectiblesCollected = 0;
                currentArea.itemAmountText.text = string.Empty;
                currentArea.UIElement3D.SetActive(false);
                Debug.Log("Generator now has " + generator.numberOfAddedParts + " parts out of " + generator.numberOfPartsNeeded);
                if (generator.numberOfAddedParts >= generator.numberOfPartsNeeded)
                {
                    SwitchLight(true);
                }
            }
            else
            {
                //You didn't collect anything that fits in here -> useless clicking sound
            }
        }
    }

    public void TryUseGenerator(Generator generator)
    {
        if (generator.correspondingRoom == currentArea.correspondingRoom)
        {
            UseCollectibles(generator);
            Debug.Log("Collectibles should have been used");
        }
        else
        {
            Debug.LogError("Something went wrong. The room of the activated generator does not match the room of the current area.");
        }
    }
}
