using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyOswald : MonoBehaviour
{

    public Material friendlyOswald;
    public Material badOswald;

    private GameManager gameManager;
    private MeshRenderer visualBlock;

    void Start()
    {
        gameManager = GameManager.gameManager;
        gameManager.LightSwitched += lightSwitch;
        visualBlock = this.gameObject.GetComponentInChildren<MeshRenderer>();
        
    }

    private void OnDestroy()
    {
        gameManager.LightSwitched -= lightSwitch;
    }

    private void lightSwitch(bool light) 
    {
        if (light)
        {
            visualBlock.material = friendlyOswald;
        } 
        else
        {
            visualBlock.material = badOswald;
        }
    }

}
