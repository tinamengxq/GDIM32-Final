using System.Collections;
using System.Collections.Generic;
using TMPro;
//using TMPro.EditorUtilities;
///using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class EndGameNotice : MonoBehaviour
{
    [SerializeField] private GameObject noticePanel;

    void Start()
    {
        noticePanel.SetActive(false);
    }

    void Update()
    {
        if (GameController.Instance.AreAllQuestsCompleted())
        {
            ShowNotice();
        }
    }

    public void ShowNotice()
    {
        noticePanel.SetActive(true);
    }
}
