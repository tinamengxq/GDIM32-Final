using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue Line", menuName = "ScriptableObjects/DialogueLine", order = 1)]
public class DialogueNode : ScriptableObject
{
    [TextArea(2,4)]
    public string[] _lines;
    public string _speakerName;
    public bool hasChoice;
    public List<DialogueChoice> choices = new List<DialogueChoice>();
    public DialogueNode nextNode;

    public string questToGive;
    public TrainingStage stageToSet = TrainingStage.NotStarted;
}

[System.Serializable]
public class DialogueChoice
{
    public string label;
    public DialogueNode nextNode;
}
