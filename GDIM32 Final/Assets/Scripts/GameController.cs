using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //Singleton
    public static GameController Instance { get; private set; }
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




    //Scripting
    public delegate void NPCAction();
    public event NPCAction QuestUpdate;
    public event NPCAction CatStateUpdate;
    public Quest quest;

    public bool playerHasToy = false;
    public bool playerHasFood = false;

    
    

    public void CompleteQuest(int questNumber)
    {
        //quest.quests[questNumber].questState = QuestState.Finished;
        QuestUpdate?.Invoke();
    }


    public void UpdateState(CatState catState)
    {
        CatStateUpdate?.Invoke();
    }


}
