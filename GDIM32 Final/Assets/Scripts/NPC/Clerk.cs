using UnityEngine;

public class Clerk : NPC
{
    [SerializeField] private DialogueNode menuNode;

    [SerializeField] private DialogueNode trainingNotStartedNode;
    [SerializeField] private DialogueNode trainingFeedAssignedNode;
    [SerializeField] private DialogueNode trainingFeedCompletedNode;
    [SerializeField] private DialogueNode trainingPlayAssignedNode;
    [SerializeField] private DialogueNode trainingPlayCompletedNode;
    [SerializeField] private DialogueNode trainingCompletedNode;

    public override void Interact()
    {
        Debug.Log("F clicked");

        if (DialogueManager.Instance == null)
        {
            Debug.LogError("[Clerk] DialogueManager not found.");
            return;
        }

        GameController controller = GameController.Instance;

        if (controller == null)
        {
            DialogueManager.Instance.StartDialogue(menuNode);
            return;
        }

        UpdateTrainingChoice(controller.CurrentTrainingStage);

        DialogueManager.Instance.StartDialogue(menuNode);
    }

    private void UpdateTrainingChoice(TrainingStage stage)
    {
        if (menuNode == null || menuNode.choices == null)
        {
            return;
        }

        for (int i = 0; i < menuNode.choices.Count; i++)
        {
            DialogueChoice choice = menuNode.choices[i];

            if (choice == null || string.IsNullOrWhiteSpace(choice.label))
            {
                continue;
            }

        
            if (choice.label.ToLower().Contains("training"))
            {
                switch (stage)
                {
                    case TrainingStage.NotStarted:
                        choice.nextNode = trainingNotStartedNode;
                        break;

                    case TrainingStage.FeedAssigned:
                        choice.nextNode = trainingFeedAssignedNode;
                        break;

                    case TrainingStage.FeedCompleted:
                        choice.nextNode = trainingFeedCompletedNode;
                        break;

                    case TrainingStage.PlayAssigned:
                        choice.nextNode = trainingPlayAssignedNode;
                        break;

                    case TrainingStage.PlayCompleted:
                        choice.nextNode = trainingPlayCompletedNode;
                        break;

                    case TrainingStage.TrainingCompleted:
                        choice.nextNode = trainingCompletedNode;
                        break;
                }
            }
        }
    }
}