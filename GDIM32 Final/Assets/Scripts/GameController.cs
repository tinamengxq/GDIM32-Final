using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    
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

    public List<int> quests = new List<int>();
    

    public delegate void NPCAction();
    public event NPCAction QuestUpdate;
    public event NPCAction CatStateUpdate;

    public void CompleteQuest(int questNumber)
    {
        if(questNumber < 0 || questNumber >= quests.Count)
        {
            return;
        }
        

        QuestUpdate?.Invoke();


    }



}
