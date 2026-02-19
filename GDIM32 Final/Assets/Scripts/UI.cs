using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    //Singleton
    public static UI Instance { get; private set; }
    public Player Player { get; private set; }

    private void Awake(){
    if (Instance != null && Instance != this){
        Destroy(this);
        return;
    }

    Instance = this;

    GameObject playerObj = GameObject.FindWithTag("Player");
    Player = playerObj.GetComponent<Player>();
    } 


    public void ProgressUI(int waitingTime)
    {
        
    }
}
