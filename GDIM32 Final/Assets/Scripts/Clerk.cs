using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clerk : NPC
{
    [SerializeField]private Animator _animator;

    public override void Interaction()
    {
        Debug.Log("CLERK INTERACTION WORKING");

        _dialogueUI.SetActive(true);
        DialogueManager.Instance.StartDialogue(dialogue);
        GameController.Instance.CompleteQuest(0);
    }
}
