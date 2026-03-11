using System;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    [SerializeField] private string defaultSpeaker = "Clerk";
    private DialogueNode currentNode;
    private int currentLine;

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
           // ShowNextLineOrFinish();
            AdvanceNodeOrFinish();
        }
    }

    public void StartDialogue(/*Dialogue dialogue*/DialogueNode startNode, Action Finished = null)
    {
        //if (dialogue == null || dialogue.sentences == null || dialogue.sentences.Length == 0)
        //{
          //  return;
        //}

        //string speaker = string.IsNullOrWhiteSpace(dialogue.nameOFNPC) ? defaultSpeaker : dialogue.nameOFNPC;
        //StartLinearDialogue(speaker, dialogue.sentences);
        ResolveReferences();
        if (dialogueView == null || startNode == null || startNode._lines == null || startNode._lines.Length == 0)
        {
            return;
        }
        OpenDialogue();
        
        //pendingLines.Clear();
        waitingForChoice = false;
        onDialogueFinished = Finished;

        currentNode = startNode;
        currentLine = 0;
        
        dialogueView.ClearChoices();
        ShowCurrentNode();

    }

    private void ShowCurrentNode()
    {
        if(currentNode == null)
        {
            FinishAndClose();
            return;
        }

        string speaker = currentNode._speakerName;
        if(string.IsNullOrWhiteSpace(speaker))
        {
            speaker = defaultSpeaker;   
        }
        currentSpeaker = speaker;

        if(currentNode._lines == null || currentNode._lines.Length == 0)
        {
            FinishAndClose();
            return;
        }
        currentLine = Mathf.Clamp(currentLine, 0, currentNode._lines.Length -1);

        //dialogueView.ClearChoices();
       //string line = currentNode._lines[currentLine];
        //bool Continue = !currentNode.hasChoice || currentNode.choices == null || currentNode.choices.Count == 0;
        bool Continue = currentLine < currentNode._lines.Length - 1;
        dialogueView.ShowNodes(currentNode, currentLine, currentSpeaker, Continue);

        if(!Continue)
        {
            if(currentNode.choices != null && currentNode.choices.Count > 0)
            {
                waitingForChoice = true;
                dialogueView.ShowNodeChoices(currentNode, OnChoiceSelected);
            }
            //waitingForChoice = true;
            //List<DialogueOption> options = BuildOptionsFromNode(currentNode);
            //dialogueView.ShowNodeChoices(currentNode, OnChoiceSelected);
            else
            {
                waitingForChoice = false;
                
            }
        }
        else
        {
            waitingForChoice = false;
        }
    }

    private List<DialogueOption> BuildOptionsFromNode(DialogueNode node)
    {
        List<DialogueOption> options = new List<DialogueOption>();
        if(node == null)
        {
            return options;
        }
        if (node.choices == null)
        {
            return options;
        }

        for(int i = 0; i < node.choices.Count; i++)
        {
            DialogueChoice choice = node.choices[i];
            if(choice == null)
            {
                continue;
            }

            string label = choice.label;
            //DialogueNode nextNode = choice.nextNode;
            DialogueOption option = new DialogueOption(label, null);
            options.Add(option);
        }
        return options;
    }

    private void FinishAndClose()
    {
        Action callBack = onDialogueFinished;
        CloseDialogue();
        if(callBack != null)
        {
            callBack.Invoke();
        }
    }

    private void AdvanceNodeOrFinish()
    {
        if(waitingForChoice)
        {
            return;
        }
        if(currentNode == null)
        {
            //ShowNextLineOrFinish();
            return;
        }
        currentLine += 1;

        if(currentLine >= currentNode._lines.Length)
        {
            FinishAndClose();
            return;
        }
        ShowCurrentNode();
        
    }

//    public void StartLinearDialogue(string speaker, IEnumerable<string> lines, Action onFinished = null)
  //  {
   //     ResolveReferences();
     //   if (dialogueView == null || lines == null)
       // {
         //   return;
        //}

//        pendingLines.Clear();
  //      foreach (string line in lines)
    //    {
      //      if (!string.IsNullOrWhiteSpace(line))
        //    {
          //      pendingLines.Enqueue(line);
            //}
//        }

  //      if (pendingLines.Count == 0)
    //    {
      //      return;
        //}

//        OpenDialogue();
   //     waitingForChoice = false;
     //   onDialogueFinished = onFinished;
      //  currentSpeaker = string.IsNullOrWhiteSpace(speaker) ? defaultSpeaker : speaker;

  //      dialogueView.ClearChoices();
//        ShowNextLineOrFinish();
    //}

  //  public void StartChoiceDialogue(string speaker, string prompt, IList<DialogueOption> options)
    //{
//        ResolveReferences();
  //      if (dialogueView == null || options == null || options.Count == 0)
    //    {
      //      return;
        //}

//        OpenDialogue();
  //      waitingForChoice = true;
    //    currentSpeaker = string.IsNullOrWhiteSpace(speaker) ? defaultSpeaker : speaker;
      //  onDialogueFinished = null;
        //pendingLines.Clear();

//        dialogueView.ShowNodes(currentNode, 0, prompt, false);
  //      dialogueView.ShowNodeChoices(options, OnChoiceSelected);
    //}

    public void CloseDialogue()
    {
        IsDialogueOpen = false;
        waitingForChoice = false;
        pendingLines.Clear();
        currentNode = null;
        onDialogueFinished = null;
        currentLine = 0;

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

//   private void ShowNextLineOrFinish()
  //  {
    //    if (pendingLines.Count == 0)
      //  {
        //    Action callback = onDialogueFinished;
          //  CloseDialogue();
            //callback?.Invoke();
           // return;
        //}

//        string line = pendingLines.Dequeue();
  //      dialogueView.ShowNodes(currentnode, currentSpeaker, line, true);
    //}

    private void OnChoiceSelected(/*DialogueOption option*/ int index)
    {
        //CloseDialogue();
        //option?.Callback?.Invoke();
        if(currentNode == null)
        {
            FinishAndClose();
            return;
        }

        if(currentNode.choices == null || currentNode.choices.Count == 0)
        {
            FinishAndClose();
            return;
        }

        if(index < 0 || index >= currentNode.choices.Count)
        {
            return;
        }

        //int index = -1;
        //for(int i = 0; i < currentNode.choices.Count; i++)
        //{
          //  DialogueChoice choice = currentNode.choices[i];
            //if(choice == null)
            //{
              //  continue;
            //}
            //if(string.Equals(choice.label, option.Label, StringComparison.Ordinal))
            //{
              //  index = i;
                //break;
            //}

        //}
       //if(index < 0)
        //{
          //  return;
        //}
        DialogueChoice Choice = currentNode.choices[index];
        if(Choice == null)
        {
            FinishAndClose();
            return;
        }

        DialogueNode next = Choice.nextNode/*currentNode.choices[index].nextNode*/;
        currentNode = next;
        currentLine = 0;

        dialogueView.ClearChoices();
        waitingForChoice = false;

        ShowCurrentNode();
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
