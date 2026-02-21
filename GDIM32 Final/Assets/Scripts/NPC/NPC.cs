using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    [SerializeField] private float interactionHeight = 1.5f;

    [SerializeField]private float interactionMaximumDistance = 3f;

    public GameObject _dialogueUI;
    public GameObject _interactionUI;
    public GameObject _questUI;
    public Transform player;

    public Dialogue dialogue;


    protected virtual void Start()
    {
        player = Camera.main.transform;

        _dialogueUI.SetActive(false);
        _interactionUI.SetActive(false);
    }

    protected virtual void Update()
    {
        if (player == null) return;

        Vector3 targetPoint = transform.position + Vector3.up * interactionHeight;

        float distance = Vector3.Distance(player.position, targetPoint);

        Vector3 directionToNPC = (targetPoint - player.position).normalized;
        float dot = Vector3.Dot(player.forward, directionToNPC);

        bool canInteract = distance <= interactionMaximumDistance && dot > 0.75f;

        _interactionUI.SetActive(canInteract);

        if (canInteract && Input.GetKeyDown(KeyCode.F))
        {
            Interaction();
        }
    }


    public virtual void Interaction()
    {
        
    }
}
