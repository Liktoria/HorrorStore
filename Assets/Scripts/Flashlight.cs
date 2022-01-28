using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]

public class Flashlight : MonoBehaviour
{
    private Light spotlight;

    // Start is called before the first frame update
    void Start()
    {
        spotlight = GetComponent<Light>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            spotlight.enabled = !spotlight.enabled;
        }
    }
}
