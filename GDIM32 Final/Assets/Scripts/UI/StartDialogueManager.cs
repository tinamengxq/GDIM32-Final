using UnityEngine;
using UnityEngine.UI;

public class StartDialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel;
    public MonoBehaviour playerController;

    void Start()
    {
        dialoguePanel.SetActive(false);
        Invoke(nameof(ShowDialoguePanel), 0.5f);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && dialoguePanel.activeSelf)
        {
            HideDialoguePanel();
        }
    }


    public void ShowDialoguePanel()
    {
        dialoguePanel.SetActive(true);
        playerController.enabled = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void HideDialoguePanel()
    {
        dialoguePanel.SetActive(false);
        playerController.enabled = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Locked;
    }
}