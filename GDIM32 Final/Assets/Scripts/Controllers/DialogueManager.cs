using System;
using System.Collections.Generic;
using UnityEngine;
//OKben 
public class DialogueManager : MonoBehaviour
{
    public sealed class DialogueOption
    {
        public string Label { get; }
        public Action Callback { get; }

        public DialogueOption(string label, Action callback)
        {
            Label = label;
            Callback = callback;
        }
    }

    public static DialogueManager Instance { get; private set; }

    [SerializeField] private Player player;
    [SerializeField] private DialogueView dialogueView;
    [SerializeField] private string defaultSpeaker = "Casual1";

    private readonly Queue<string> pendingLines = new Queue<string>();
    private string currentSpeaker;
    private Action onDialogueFinished;
    private bool waitingForChoice;

    public bool IsDialogueOpen { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        ResolveReferences();
    }

    private void Start()
    {
        ResolveReferences();
        if (dialogueView != null)
        {
            dialogueView.SetVisible(false);
        }
    }

    private void Update()
    {
        if (!IsDialogueOpen || waitingForChoice)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            ShowNextLineOrFinish();
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        if (dialogue == null || dialogue.sentences == null || dialogue.sentences.Length == 0)
        {
            return;
        }

        string speaker = string.IsNullOrWhiteSpace(dialogue.nameOFNPC) ? defaultSpeaker : dialogue.nameOFNPC;
        StartLinearDialogue(speaker, dialogue.sentences);
    }

    public void StartLinearDialogue(string speaker, IEnumerable<string> lines, Action onFinished = null)
    {
        ResolveReferences();
        if (dialogueView == null || lines == null)
        {
            return;
        }

        pendingLines.Clear();
        foreach (string line in lines)
        {
            if (!string.IsNullOrWhiteSpace(line))
            {
                pendingLines.Enqueue(line);
            }
        }

        if (pendingLines.Count == 0)
        {
            return;
        }

        OpenDialogue();
        waitingForChoice = false;
        onDialogueFinished = onFinished;
        currentSpeaker = string.IsNullOrWhiteSpace(speaker) ? defaultSpeaker : speaker;

        dialogueView.ClearChoices();
        ShowNextLineOrFinish();
    }

    public void StartChoiceDialogue(string speaker, string prompt, IList<DialogueOption> options)
    {
        ResolveReferences();
        if (dialogueView == null || options == null || options.Count == 0)
        {
            return;
        }

        OpenDialogue();
        waitingForChoice = true;
        currentSpeaker = string.IsNullOrWhiteSpace(speaker) ? defaultSpeaker : speaker;
        onDialogueFinished = null;
        pendingLines.Clear();

        dialogueView.ShowLine(currentSpeaker, prompt, false);
        dialogueView.ShowChoices(options, OnChoiceSelected);
    }

    public void CloseDialogue()
    {
        IsDialogueOpen = false;
        waitingForChoice = false;
        pendingLines.Clear();
        onDialogueFinished = null;

        if (dialogueView != null)
        {
            dialogueView.ClearChoices();
            dialogueView.SetVisible(false);
        }

        if (player != null)
        {
            player.SetControlEnabled(true);
        }
    }

    private void OpenDialogue()
    {
        ResolveReferences();
        if (dialogueView == null)
        {
            return;
        }

        IsDialogueOpen = true;
        dialogueView.SetVisible(true);

        if (player != null)
        {
            player.SetControlEnabled(false);
        }
    }

    private void ShowNextLineOrFinish()
    {
        if (pendingLines.Count == 0)
        {
            Action callback = onDialogueFinished;
            CloseDialogue();
            callback?.Invoke();
            return;
        }

        string line = pendingLines.Dequeue();
        dialogueView.ShowLine(currentSpeaker, line, true);
    }

    private void OnChoiceSelected(DialogueOption option)
    {
        CloseDialogue();
        option?.Callback?.Invoke();
    }

    private void ResolveReferences()
    {
        if (player == null)
        {
            if (GameController.Instance != null)
            {
                player = GameController.Instance.Player;
            }

            if (player == null)
            {
                player = FindObjectOfType<Player>();
            }
        }

        if (dialogueView == null && UI.Instance != null)
        {
            dialogueView = UI.Instance.DialogueView;
        }

        if (dialogueView == null)
        {
            dialogueView = FindObjectOfType<DialogueView>(true);
        }
    }
}
