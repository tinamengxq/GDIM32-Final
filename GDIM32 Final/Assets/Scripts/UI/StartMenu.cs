using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public MonoBehaviour playerController;

    void Start()
    {
        Time.timeScale = 0f;

        playerController.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void StartGame()
    {
        gameObject.SetActive(false);

        Time.timeScale = 1f;

        playerController.enabled = true;

        if (playerController is Player p)
            p.FixCamLocal();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}