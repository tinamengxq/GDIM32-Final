using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clerk : NPC
{
    [SerializeField]private Animator _animator;

    public override void Interaction()
    {
        _dialogueUI.SetActive(true);
        GameController.Instance.CompleteQuest(0);
    }
}
