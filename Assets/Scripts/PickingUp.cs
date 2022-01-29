using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickingUp : MonoBehaviour
{
    [SerializeField] private Image inventoryImage;
    private List<GameObject> detectedInteractables = new List<GameObject>();
    private float distanceToInteractable;
    private float smallestDistanceToInteractable;
    private int indexOfClosestInteractable = 0;
    private InteractableType currentItem = InteractableType.NONE;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (detectedInteractables.Count > 0)
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
                inventoryImage.sprite = closestInteractable.GetComponent<Interactable>().inventoryIcon;
                currentItem = closestInteractable.GetComponent<Interactable>().typeOfInteractable;
                Destroy(closestInteractable);
                detectedInteractables.RemoveAt(indexOfClosestInteractable);
                indexOfClosestInteractable = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Interactable")
        {
            Debug.Log("Found " + other.gameObject.name);
            if (!detectedInteractables.Contains(other.gameObject))
            {
                detectedInteractables.Add(other.gameObject);
                Debug.Log("Added interactable to list.");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Interactable")
        {
            detectedInteractables.Remove(other.gameObject);
            Debug.Log("Removed interactable from list.");
        }
    }
}
