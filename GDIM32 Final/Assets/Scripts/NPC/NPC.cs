using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] private float interactionHeight = 1.5f;
    [SerializeField] private float interactionMaximumDistance = 3f;
    [SerializeField] private string promptText = "F: Interact";

    public virtual string GetPromptText()
    {
        return promptText;
    }

    public virtual Transform GetInteractionTransform()
    {
        return transform;
    }

    public virtual float GetMaxDistance()
    {
        return interactionMaximumDistance;
    }

    public virtual bool CanInteract()
    {
        if (!enabled || !gameObject.activeInHierarchy)
        {
            return false;
        }

        // Skip the plain base component to avoid double interaction on grouping objects.
        if (GetType() == typeof(NPC))
        {
            return false;
        }

        return true;
    }

    public virtual void Interact()
    {
    }

    public virtual Vector3 GetInteractionPoint()
    {
        return transform.position + Vector3.up * interactionHeight;
    }
}
