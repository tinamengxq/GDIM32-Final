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
    public string questContent;
    public QuestState questState;

    public List<string> quests = new List<string>();
    
    public void ListofQuests()
    {
        quests.Add(questContent = "Feed The Cat.");
        quests.Add(questContent = "Play With The Cat.");
    }

    
}
