using UnityEngine;

public class CatFood : Item
{
    public override string GetPromptText()
    {
        return "F: Pick Cat Food";
    }

    public override void Interact()
    {
        base.Interact();
    }

    protected override bool ShouldBeVisible(GameController controller)
    {
        return controller.CurrentTrainingStage == TrainingStage.FeedAssigned && !controller.HasCatFood;
    }

    protected override void OnInteract(GameController controller)
    {
        controller.SetHasCatFood(true);
        Debug.Log("[CatFood] Picked up cat food.");
    }
}
