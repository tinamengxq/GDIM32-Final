using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CatState
{
    NotSatisfied,
    Eating,
    Playing,
    Satisfied

}
public class Cat : NPC
{
    [SerializeField]private Animator _animator;
    [SerializeField]private float eatingTime = 5f;
    [SerializeField]private float playingTime = 5f;

    public CatState catState = CatState.NotSatisfied;



    public override void Interaction()
    {
        if(GameController.Instance.playerHasToy && catState == CatState.NotSatisfied)
        {
            JustDoIt("Play", CatState.Playing, 1);
        }
        else if(GameController.Instance.playerHasFood && catState == CatState.NotSatisfied)
        {
            JustDoIt("Eat", CatState.Eating, 0);
        }
    }

    private void JustDoIt(string animatorString, CatState newState, int questNumber)
    {
        //_animator.SetString(animatorString);
        catState = newState;
        UI.Instance.ProgressUI(5);
        GameController.Instance.CompleteQuest(questNumber);

        float time = 5f;
        float newtime = time - Time.deltaTime;
        if(newtime <= 0f && questNumber == 0)
        {
            catState = CatState.NotSatisfied;
        }
        else if(newtime <= 0f && questNumber == 1)
        {
            catState = CatState.Satisfied;
        }
        
    }

}
