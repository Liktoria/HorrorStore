using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interact : MonoBehaviour
{
    private List<GameObject> detectedInteractables = new List<GameObject>();
    private float distanceToInteractable;
    private float smallestDistanceToInteractable;
    private int indexOfClosestInteractable = 0;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.gameManager;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && detectedInteractables.Count > 0)
        {
            smallestDistanceToInteractable = Vector3.Distance(transform.position, detectedInteractables[0].transform.position);
            for (int i = 0; i < detectedInteractables.Count; i++)
            {
                distanceToInteractable = Vector3.Distance(transform.position, detectedInteractables[i].transform.position);
                if (distanceToInteractable < smallestDistanceToInteractable)
                {
                    smallestDistanceToInteractable = distanceToInteractable;
                    indexOfClosestInteractable = i;
                }
            }
            GameObject closestInteractable = detectedInteractables[indexOfClosestInteractable];
            ProcessInteractable(closestInteractable);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Interactable")
        {
            if (!detectedInteractables.Contains(other.gameObject))
            {
                detectedInteractables.Add(other.gameObject);
                //Debug.Log("Added interactable to list.");
            }
        }
        else if(other.gameObject.tag == "Oswald")
        {
            //TODO: (Sound) Add creepy "oswald discovered behind you" sound
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Interactable")
        {
            detectedInteractables.Remove(other.gameObject);
            //Debug.Log("Removed interactable from list.");
        }
    }

    private void ProcessInteractable(GameObject interactable)
    {
        Interactable currentInteractable = interactable.GetComponent<Interactable>();
        if (currentInteractable is Generator generator)
        {
            Debug.Log("Interacted with a generator.");
            gameManager.TryUseGenerator(generator);
            gameManager.CheckWin();
        }
        else if (currentInteractable is Box box)
        {
            //Open Box and check for contents
            //Debug.Log("Found a box, something should happen.");
            detectedInteractables.Remove(box.gameObject);
            gameManager.OpenBox(box);
        }
        else if (currentInteractable is Collectible collectible)
        {
            //Add collected item to game manager
            gameManager.Collect(collectible);
            Destroy(interactable);
            detectedInteractables.RemoveAt(indexOfClosestInteractable);
            indexOfClosestInteractable = 0;
        }
    }
}
