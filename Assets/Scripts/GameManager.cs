using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    public List<AreaManager> areas;
    public AreaManager currentArea;

    public static GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = this;
        if(areas.Count > 0)
        {
            currentArea = areas[0];
        }
        else
        {
            throw new UnityException("Es muss mindestens eine Area im Gamemanager konfiguriert sein.");
        }
    }


    [System.Serializable]
    public class AreaManager
    {
        public bool lightOn = false;
    }
}
