using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchArea : MonoBehaviour
{
    [SerializeField] private Room oneRoom;
    [SerializeField] private Room secondRoom;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.gameManager;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            AreaManager previousArea = gameManager.currentArea;
            if(gameManager.currentArea.correspondingRoom == oneRoom)
            {
                gameManager.currentArea = gameManager.areas[(int)secondRoom];
                Debug.Log("Current area is now " + gameManager.currentArea.correspondingRoom);
            }
            else if(gameManager.currentArea.correspondingRoom == secondRoom)
            {
                gameManager.currentArea = gameManager.areas[(int)oneRoom];
                Debug.Log("Current area is now " + gameManager.currentArea.correspondingRoom);
            }
            if(!gameManager.currentArea.lightOn && previousArea.lightOn)
            {
                gameManager.SwitchLight(false);
            }
            else if(gameManager.currentArea.lightOn && !previousArea.lightOn)
            {
                gameManager.SwitchLight(true);
            }
        }
    }
}
