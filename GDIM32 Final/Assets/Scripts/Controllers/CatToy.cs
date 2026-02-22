using UnityEngine;

public class CatToy : Item
{
    public override string GetPromptText()
    {
        return "F: Pick Cat Toy";
    }

    public override void Interact()
    {
        base.Interact();
    }

    protected override bool ShouldBeVisible(GameController controller)
    {
        return controller.CurrentTrainingStage == TrainingStage.PlayAssigned && !controller.HasCatToy;
    }

    protected override void OnInteract(GameController controller)
    {
        controller.SetHasCatToy(true);
        Debug.Log("[CatToy] Picked up cat toy.");
    }
}
