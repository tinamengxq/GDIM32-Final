using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue Line", menuName = "ScriptableObjects/DialogueLine", order = 1)]
public class DialogueNode : ScriptableObject
{
    public string _lines;
    public string _speakerName;
    public bool hasChoice;
    public List<DialogueChoice> choices = new List<DialogueChoice>();
    public DialogueNode nextNode;
}

[System.Serializable]
public class DialogueChoice
{
    public string label;
    public DialogueNode nextNode;
}
