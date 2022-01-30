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

    private bool gameWon;

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

        gameWon = false;
    }

    private void Start()
    {
        SwitchLight(true);
    }

    public void SwitchLight(bool lightOn)
    {
        currentArea.lightOn = lightOn;
        Debug.Log("Setting light to " + lightOn + " in area " + currentArea.correspondingRoom);
        if(lightOn)
        {
            StartCoroutine(LightSwitchSmooth(0.3f, 1.5f, 2));
            RenderSettings.reflectionIntensity = 0.5f;
        }
        else
        {
            StartCoroutine(LightSwitchSmooth(1.5f, 0.3f, 2));
            RenderSettings.reflectionIntensity = 0.2f;
        }
        LightSwitched?.Invoke(lightOn, currentArea.correspondingRoom);
        //TODO: (Sound) if(lightOn = true) -> Switching on lights, start music, evtl. summen, stop creepy stuff in room
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
        //TODO: (Sound) Picking things up
    }

    private void UseCollectibles(Generator generator)
    {
        if (!currentArea.lightOn)
        {
            if (currentArea.collectiblesCollected > 0)
            {
                generator.AddFuses(currentArea.collectiblesCollected);
                //TODO: (Sound) Sicherung in Sicherungskasten einbauen
                currentArea.collectiblesCollected = 0;
                currentArea.itemAmountText.text = string.Empty;
                currentArea.UIElement3D.SetActive(false);
                Debug.Log("Generator now has " + generator.numberOfAddedParts + " parts out of " + generator.numberOfPartsNeeded);
                if (generator.numberOfAddedParts >= generator.numberOfPartsNeeded)
                {
                    SwitchLight(true);
                }
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
            GameObject newBox = Instantiate(openBox, box.transform.position, box.transform.rotation, box.transform.parent);
            newBox.SetActive(true);            
            Destroy(box.gameObject);
            if (hasCollectible)
            {
                GameObject spawnedCollectible = Instantiate(collectible, spawnLocation.position, Quaternion.identity);
                spawnedCollectible.SetActive(true);
                spawnedCollectible.GetComponent<Rigidbody>().AddForce(forceDirection * 0.1f);
                //TODO: (Sound) Box opening with item jumping out
            }
            else
            {
                //TODO: (Sound) Empty box opening
            }
        }        
    }

    IEnumerator LightSwitchSmooth(float startValue, float endValue, float duration)
    {
        float time = 0;

        while (time < duration)
        {
            RenderSettings.ambientIntensity = Mathf.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }        
    }

    public bool CheckWin()
    {
        int lights = 0;
        for(int i = 0; i < areas.Count; i++)
        {
            if(areas[i].lightOn)
                lights++;

            if(lights == areas.Count)
                gameWon = true;
        }
        return gameWon;
    }
}
