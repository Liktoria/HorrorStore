using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    [SerializeField] private Room room;
    private Light light;
    private Material lightMat;
    private GameManager gameManager;
    private bool lightIsOn;

    private bool isFlickering;
    private float flickerDelay;

    void Start()
    {
        light = GetComponentInChildren<Light>();
        lightMat = GetComponentInChildren<Renderer>().material;
        ToggleLight(false, Room.MAINTENANCE_ROOM);
        gameManager = GameManager.gameManager;
        gameManager.LightSwitched += ToggleLight;
    }

    private void ToggleLight(bool lightOn, Room currentRoom)
    {       
        if (currentRoom == room)
        {
            lightIsOn = lightOn;
            if (lightOn)
            {
                StartCoroutine(LightFlicker());
                StartCoroutine(LightSwitchSmooth(0.5f, 1.5f, 2));
                lightMat.EnableKeyword("_EMISSION");
                //RenderSettings.ambientIntensity = 1.5f;
                //RenderSettings.reflectionIntensity = 0.5f;
            }
            else
            {
                light.enabled = false;
                lightMat.DisableKeyword("_EMISSION");
                StartCoroutine(LightSwitchSmooth(1.5f, 0.5f, 2));
                //RenderSettings.ambientIntensity = 0.5f;
                //RenderSettings.reflectionIntensity = 0.2f;
            }
        }   
        else if(currentRoom == Room.MAINTENANCE_ROOM)
        {
            lightIsOn = lightOn;
            if (lightOn)
            {
                StartCoroutine(LightFlicker());
                StartCoroutine(LightSwitchSmooth(0.5f, 1.5f, 2));
                lightMat.EnableKeyword("_EMISSION");
                //RenderSettings.ambientIntensity = 1.5f;
                //RenderSettings.reflectionIntensity = 0.5f;
            }
            else
            {
                light.enabled = false;
                lightMat.DisableKeyword("_EMISSION");
                StartCoroutine(LightSwitchSmooth(1.5f, 0.5f, 2));
                //RenderSettings.ambientIntensity = 0.5f;
                //RenderSettings.reflectionIntensity = 0.2f;
            }
        }
    }

    IEnumerator LightFlicker()
    {
        int flicker = Random.Range(1, 10);
        for(int i = 0; i < flicker; i++)
        {
            flickerDelay = Random.Range(0.05f, 0.2f);
            yield return new WaitForSeconds(flickerDelay);
            if(i % 2 == 0)
                light.enabled = true;
            else
                light.enabled = false;
        }
        light.enabled = true;
    }

    IEnumerator LightSwitchSmooth(float startValue, float endValue, float duration)
    {
        float time = 0;

        while(time < duration)
        {
            RenderSettings.ambientIntensity = Mathf.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
    }
}
