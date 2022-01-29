using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject openBox;
    [SerializeField] private GameObject collectible;
    public List<AreaManager> areas;
    [HideInInspector] public AreaManager currentArea;
    public event Action<bool, Room> LightSwitched;    

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
                generator.AddFuses(currentArea.collectiblesCollected);
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

    public void OpenBox(Box box)
    {
        if(!box.open)
        {
            //Open box
            box.open = true;
            bool hasCollectible = box.hasCollectible;
            Transform spawnLocation = box.collectibleSpawnLocation;
            Vector3 forceDirection = box.forceDirection;
            GameObject newBox = Instantiate(openBox, box.transform.position, openBox.transform.rotation, box.transform.parent);
            newBox.SetActive(true);            
            Destroy(box.gameObject);
            if (hasCollectible)
            {
                GameObject spawnedCollectible = Instantiate(collectible, spawnLocation.position, Quaternion.identity);
                spawnedCollectible.SetActive(true);
                spawnedCollectible.GetComponent<Rigidbody>().AddForce(forceDirection);
                //TODO: Box opening sound with item jumping out
            }
            else
            {
                //TODO: Empty box opening sound
            }
        }
        else
        {
            
        }
    }
}
