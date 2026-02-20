using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{

    [SerializeField]private float interactionMaximumDistance = 1f;

    public GameObject _dialogueUI;
    public GameObject _interactionUI;
    public GameObject _questUI;
    public Transform player;


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
            _interactionUI.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
        {
            _interactionUI.SetActive(false);
            Interaction();
        }
        }
        else
        {
            _interactionUI.SetActive(false);
        }
        
    }

    public virtual void Interaction()
    {
        
    }
}
