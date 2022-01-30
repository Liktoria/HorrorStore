using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSound : MonoBehaviour
{
    private float secondsUntilSoundsStart = 1f;
    private bool shouldSoundsPlay = false;
    [SerializeField] FMODUnity.StudioEventEmitter eventEmitter;
    //TODO: (Sound) Add variable/reference here, that can be set from outside to determine which sound should be played on collision

    private void Start()
    {
        StartCoroutine(WaitBeforeSoundsStart(secondsUntilSoundsStart));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (shouldSoundsPlay)
        {
            //TODO: (Sound) play collision sound of objects (krachen/scheppern)
            eventEmitter.Play();
        }
    }

    private IEnumerator WaitBeforeSoundsStart(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        shouldSoundsPlay = true;
    }
}
