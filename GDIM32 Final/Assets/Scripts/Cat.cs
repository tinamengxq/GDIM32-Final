using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : NPC
{
    [SerializeField]private Animator _animator;
    [SerializeField]private float eatingTime = 5f;
    [SerializeField]private float playingTime = 5f;



    public override void Interaction()
    {
        
    }

}
