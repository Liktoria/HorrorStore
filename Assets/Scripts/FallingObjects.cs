using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjects : MonoBehaviour
{
    public GameObject fallingObject;
    public Vector3 fallingForceStrength;
    public bool isObjectFallingDown;
    public float eventProbability;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // only call with given probability
            float probability = Random.Range(0f, 1f);

            if (probability < eventProbability)
            {
                if (isObjectFallingDown)
                    fallingObject.GetComponent<Rigidbody>().useGravity = true;

                fallingObject.GetComponent<Rigidbody>().AddForce(fallingForceStrength, ForceMode.Impulse);

                gameObject.SetActive(false);
            }
        }
    }
}
