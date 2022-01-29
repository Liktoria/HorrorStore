using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AreaManager
{
    public bool lightOn = false;
    public int collectiblesCollected = 0;
    public int collectiblesNeeded;
    public Room correspondingRoom;
    public bool oswaldSpawnAllowed = false;
}

