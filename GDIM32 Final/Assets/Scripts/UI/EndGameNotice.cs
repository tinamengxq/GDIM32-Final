using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
//using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameNotice : MonoBehaviour
{
    [SerializeField] public GameObject noticePanel;
    [SerializeField] private MonoBehaviour player;

    private bool decided;
    public bool doShowInteraction;

    void Start()
    {
        noticePanel.SetActive(false);
        doShowInteraction = true;
    }

    void Update()
    {

       // if (GameController.Instance.AreAllQuestsCompleted())
        //{
          //  ShowNotice();
        //}
        ///if(noticePanel.activeSelf && Input.GetKeyDown(KeyCode.R))
        //{
         //   RestartGame();
        //}
        if (noticePanel.activeSelf && Input.GetKeyDown(KeyCode.C))
        {
            ContinueGame();
        }
    }


    public void ShowNotice()
    {
        noticePanel.SetActive(true);
        player.enabled = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        doShowInteraction = false;
    }
    
   // public void RestartGame()
   // {
   //     Debug.Log("RestartGame called");
   //     Time.timeScale = 1f;
   //     SceneManager.LoadScene("SampleScene");
   // }

    public void ContinueGame()
    {
        Debug.Log("ContinueGame called");
        noticePanel.SetActive(false);
        player.enabled = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Locked;
        decided = true;
        //Destroy(noticePanel);
        doShowInteraction = true;
    }
}
