using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : Interactable
{
    public bool open = false;
    public bool hasCollectible = false;
    public Transform collectibleSpawnLocation;
}
