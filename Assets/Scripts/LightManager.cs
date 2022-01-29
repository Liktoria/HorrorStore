using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
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
        gameManager = GameManager.gameManager;
        gameManager.LightSwitched += ToggleLight;
    }

    private void ToggleLight(bool lightOn)
    {
        lightIsOn = lightOn;

        if(lightIsOn)
        {
            StartCoroutine(LightFlicker());
            lightMat.EnableKeyword("_EMISSION");
        } 
        else if(!lightIsOn)
        {
            light.enabled = false;
            lightMat.DisableKeyword("_EMISSION");
        }
            
        
    }

    IEnumerator LightFlicker()
    {
        int flicker = Random.Range(1, 5);
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
}
