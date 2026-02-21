using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]private GameController gameController;

    private Queue<string> sentences;
  
    public static DialogueManager Instance { get; private set; }            
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

    void Start()
    {
        sentences = new Queue<string>();
        //gameController.Startdialogue += StartDialogue;
    }

    public void StartDialogue(Dialogue dialogue)
    {
        
    }

}
