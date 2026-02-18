using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    [SerializeField]private float interactionMaximumDistance = 5f;
    public GameObject _dialogueUI;
    public GameObject _interactionUI;

    public Transform player;
    private bool playerIsClose = false;


    void Start()
    {
        _dialogueUI.SetActive(false);
        _interactionUI.SetActive(false);
    }

    void Update()
    {
        float distancePlayerNPC = Vector3.Distance(transform.position, player.transform.position);
        if (distancePlayerNPC <= interactionMaximumDistance)
        {
            playerIsClose = true;
            _interactionUI.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
        {
            EnableDialogue();
        }
        }
        else
        {
            playerIsClose = false;
            _interactionUI.SetActive(false);
        }
        
    }
    void EnableDialogue()
    {
        _dialogueUI.SetActive(true);
    }

    public virtual void UniqueBehavior()
    {
        
    }
}
