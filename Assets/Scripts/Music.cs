using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField] private FMODUnity.StudioParameterTrigger parameterTriggerMain;
    [SerializeField] private FMODUnity.StudioParameterTrigger parameterTriggerIntro;
    [SerializeField] private FMODUnity.StudioParameterTrigger parameterTriggerOutro;
    [SerializeField] private FMODUnity.StudioEventEmitter eventEmitter;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.gameManager.LightSwitched += LightSwitched;
    }

    private void LightSwitched(bool lightOn, Room currentRoom)
    {
        if(lightOn)
        {
            eventEmitter.Play();
            parameterTriggerMain.TriggerParameters();
        }
        else
        {
            parameterTriggerOutro.TriggerParameters();
        }
    }
}
