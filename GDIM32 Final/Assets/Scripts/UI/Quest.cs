using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestState
{
    Opened,
    Doing,
    Finished
}

public class Quest : MonoBehaviour
{
    public static Quest Instance; 
    public string questContent;
    public QuestState questState;

    public List<string> quests = new List<string>();

    private void Awake()
    {
        Instance = this;  
    }

    public void AcceptQuest(string questName)
    {
        quests.Add(questName);
        Debug.Log("Quest Accepted: " + questName);
    }

    public void ListofQuests()
    {
        quests.Add("Feed The Cat.");
        quests.Add("Play With The Cat.");
    }
}