using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<AreaManager> areas;
    public AreaManager currentArea;

    public static GameManager gameManager;

    private void Awake()
    {
        if(gameManager == null)
        {
            gameManager = this;
        }

        if (areas.Count > 0)
        {
            currentArea = areas[0];
        }
        else
        {
            throw new UnityException("Es muss mindestens eine Area im Gamemanager konfiguriert sein.");
        }
    }    
}
