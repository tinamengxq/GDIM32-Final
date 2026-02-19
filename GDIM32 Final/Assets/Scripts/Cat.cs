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
        
    }

}
