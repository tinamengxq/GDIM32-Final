using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        
    }
}
