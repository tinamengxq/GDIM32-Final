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

    //protected override bool ShouldBeVisible(GameController controller)
    //{
    // return controller.CurrentTrainingStage == TrainingStage.FeedAssigned && !controller.HasCatFood;
    // }
    protected override bool ShouldBeVisible(GameController controller)
    {
        // bool visible = controller.CurrentTrainingStage == TrainingStage.FeedAssigned && !controller.HasCatFood;
        // Debug.Log("[CatFood] visible = " + visible + " | stage = " + controller.CurrentTrainingStage + " | hasFood = " + controller.HasCatFood);
        // return visible;
        return controller.CurrentTrainingStage == TrainingStage.FeedAssigned && !controller.HasCatFood;
        //return true;
    }
    protected override void OnInteract(GameController controller)
    {
        Debug.Log("[CatFood] OnInteract called");
        controller.SetHasCatFood(true);
        Debug.Log("[CatFood] Picked up cat food.");
        gameObject.SetActive(false);
    }
}
