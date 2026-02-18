using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{

    [SerializeField]private float interactionMaximumDistance = 5f;

    public GameObject _dialogueUI;
    public GameObject _interactionUI;
    public GameObject _questUI;
    public Transform player;


    private bool playerIsClose;


    protected virtual void Start()
    {
        _dialogueUI.SetActive(false);
        _interactionUI.SetActive(false);
    }

    protected virtual void Update()
    {
        float distancePlayerNPC = Vector3.Distance(transform.position, player.transform.position);
        if (distancePlayerNPC <= interactionMaximumDistance)
        {
            playerIsClose = true;
            _interactionUI.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
        {
            Interaction();
        }
        }
        else
        {
            playerIsClose = false;
            _interactionUI.SetActive(false);
        }
        
    }

    public virtual void Interaction()
    {
        
    }
}
