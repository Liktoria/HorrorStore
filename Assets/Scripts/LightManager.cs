using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    [SerializeField] private Room room;
    private Light light;
    [SerializeField] private Material lightMat;
    private GameManager gameManager;
    private bool lightIsOn;

    private bool isFlickering;
    private float flickerDelay;

    void Start()
    {
        light = GetComponentInChildren<Light>();
        GetComponentInChildren<Renderer>().material = lightMat;
        gameManager = GameManager.gameManager;
        gameManager.LightSwitched += ToggleLight;
        ToggleLight(false, room);
    }

    private void ToggleLight(bool lightOn, Room currentRoom)
    {
        if (currentRoom == room && room != Room.MAINTENANCE_ROOM)
        {
            lightIsOn = lightOn;
            if (lightOn)
            {
                StartCoroutine(LightFlicker());
                lightMat.EnableKeyword("_EMISSION");
                //RenderSettings.ambientIntensity = 1.5f;
                //RenderSettings.reflectionIntensity = 0.5f;
            }
            else
            {
                light.enabled = false;
                lightMat.DisableKeyword("_EMISSION");
                //RenderSettings.ambientIntensity = 0.5f;
                //RenderSettings.reflectionIntensity = 0.2f;
            }
        }        
    }

    IEnumerator LightFlicker()
    {
        int flicker = Random.Range(1, 10);
        for (int i = 0; i < flicker; i++)
        {
            flickerDelay = Random.Range(0.05f, 0.2f);
            yield return new WaitForSeconds(flickerDelay);
            if (i % 2 == 0)
                light.enabled = true;
            else
                light.enabled = false;
        }
        light.enabled = true;
    }
}
