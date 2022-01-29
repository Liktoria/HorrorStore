using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PappaufstellerDespawn : MonoBehaviour
{

    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.gameManager;
        gameManager.LightSwitched += ToggleLight;
    }

    private void OnDestroy()
    {
        gameManager.LightSwitched -= ToggleLight;
    }

    private void ToggleLight(bool lightOn)
    {
        Destroy(this);
    }
}
