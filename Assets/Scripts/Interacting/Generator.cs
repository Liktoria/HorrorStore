using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : Interactable
{
    [HideInInspector] public int numberOfPartsNeeded;
    [HideInInspector] public int numberOfAddedParts = 0;
    public List<GameObject> missingFuses = new List<GameObject>();

    private void Start()
    {
        foreach(GameObject missingFuse in missingFuses)
        {
            missingFuse.SetActive(false);
        }
        numberOfPartsNeeded = missingFuses.Count;
    }

    public void AddFuses(int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            missingFuses[0].SetActive(true);
            missingFuses.RemoveAt(0);
        }
        numberOfAddedParts += amount;
    }
}
