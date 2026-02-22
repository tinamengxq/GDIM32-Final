using System.Collections.Generic;
using UnityEngine;

public class Clerk : NPC
{
    [SerializeField] private string speakerName = "Casual1";

    public override void Interact()
    {
        if (DialogueManager.Instance == null)
        {
            Debug.LogError("[Clerk] DialogueManager not found.");
            return;
        }

        List<DialogueManager.DialogueOption> options = new List<DialogueManager.DialogueOption>
        {
            new DialogueManager.DialogueOption("Quest Training", OnQuestTrainingSelected),
            new DialogueManager.DialogueOption("Cat Knowledge", OnCatKnowledgeSelected)
        };

        DialogueManager.Instance.StartChoiceDialogue(speakerName, "What do you want to ask?", options);
    }

    private void OnQuestTrainingSelected()
    {
        GameController controller = GameController.Instance;
        if (controller == null)
        {
            return;
        }

        switch (controller.CurrentTrainingStage)
        {
            case TrainingStage.NotStarted:
                DialogueManager.Instance.StartLinearDialogue(
                    speakerName,
                    new[]
                    {
                        "You're here? How are you settling in with the kitten?",
                        "That's good! Don't slack off. If you can't prove you can take care of her, you might lose her~",
                        "The most important thing about raising kittens is feeding them well. Now go home and feed your cat!"
                    },
                    controller.AssignFeedQuest
                );
                break;

            case TrainingStage.FeedAssigned:
                DialogueManager.Instance.StartLinearDialogue(
                    speakerName,
                    new[]
                    {
                        "Has she eaten enough?"
                    }
                );
                break;

            case TrainingStage.FeedCompleted:
                DialogueManager.Instance.StartLinearDialogue(
                    speakerName,
                    new[]
                    {
                        "You're back? Great! She's full now, so it's time to learn the most important skill for a cat owner! Play with her!"
                    },
                    controller.AssignPlayQuest
                );
                break;

            case TrainingStage.PlayAssigned:
                DialogueManager.Instance.StartLinearDialogue(
                    speakerName,
                    new[]
                    {
                        "Are you having a good time?"
                    }
                );
                break;

            case TrainingStage.PlayCompleted:
                DialogueManager.Instance.StartLinearDialogue(
                    speakerName,
                    new[]
                    {
                        "You have completed your training as a new parent, and I believe you are now fully prepared. Thank you for choosing to be her mother. I wish you both peace and joy as you journey through every season together!"
                    },
                    controller.MarkTrainingCompleted
                );
                break;

            case TrainingStage.TrainingCompleted:
                DialogueManager.Instance.StartLinearDialogue(
                    speakerName,
                    new[]
                    {
                        "You have completed your training as a new parent, and I believe you are now fully prepared. Thank you for choosing to be her mother. I wish you both peace and joy as you journey through every season together!"
                    }
                );
                break;
        }
    }

    private void OnCatKnowledgeSelected()
    {
        DialogueManager.Instance.StartLinearDialogue(
            speakerName,
            new[]
            {
                "Want to learn more about cat ?",
                "Cats swallow their food rather than chew it.",
                "The functions of cat whiskers: They help cats hunt, sense humidity and airflow, measure distance, and maintain balance.",
                "Cats rarely meow at other cats. They may growl, hiss, or purr at other felines, but meowing is mostly reserved for humans.",
                "Do not keep cats in cages, as this can hinder their growth and development and cause them to become timid and irritable.",
                "A cat's nose print is unique, similar to a human fingerprint."
            }
        );
    }
}
