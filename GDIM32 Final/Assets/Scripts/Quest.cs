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

    public List<Quest> quests = new List<Quest>();

    
}
