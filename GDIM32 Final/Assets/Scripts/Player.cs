using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Player : MonoBehaviour
{
    [SerializeField]private float playerSpeed = 5f;
    [SerializeField]private float jumpHeight = 1f;

    void Update()
    {
        Move();
    }

    void Move()
    {
        
    }
}
