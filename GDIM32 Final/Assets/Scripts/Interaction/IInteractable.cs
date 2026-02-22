using UnityEngine;

public interface IInteractable
{
    string GetPromptText();
    Transform GetInteractionTransform();
    float GetMaxDistance();
    void Interact();
    bool CanInteract();
}
