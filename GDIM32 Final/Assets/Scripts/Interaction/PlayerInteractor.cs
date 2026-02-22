using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float viewDotThreshold = 0.65f;
    [SerializeField] private float fallbackMaxDistance = 4f;

    private IInteractable currentTarget;

    private void Awake()
    {
        if (cameraTransform == null)
        {
            Player player = GetComponent<Player>();
            if (player != null)
            {
                cameraTransform = player.CameraTransform;
            }
        }

        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    private void Update()
    {
        if (cameraTransform == null)
        {
            return;
        }

        if (DialogueManager.Instance != null && DialogueManager.Instance.IsDialogueOpen)
        {
            currentTarget = null;
            UI.Instance?.HideInteractPrompt();
            return;
        }

        currentTarget = FindBestInteractable();
        if (currentTarget == null)
        {
            UI.Instance?.HideInteractPrompt();
            return;
        }

        string prompt = currentTarget.GetPromptText();
        if (string.IsNullOrWhiteSpace(prompt))
        {
            prompt = "F: Interact";
        }

        UI.Instance?.ShowInteractPrompt(prompt);

        if (Input.GetKeyDown(KeyCode.F))
        {
            currentTarget.Interact();
        }
    }

    public void SetCameraTransform(Transform targetCamera)
    {
        if (targetCamera != null)
        {
            cameraTransform = targetCamera;
        }
    }

    private IInteractable FindBestInteractable()
    {
        MonoBehaviour[] behaviours = FindObjectsOfType<MonoBehaviour>();
        Vector3 origin = cameraTransform.position;
        Vector3 forward = cameraTransform.forward;

        IInteractable best = null;
        float bestScore = float.NegativeInfinity;

        for (int i = 0; i < behaviours.Length; i++)
        {
            MonoBehaviour behaviour = behaviours[i];
            if (behaviour == null || !behaviour.isActiveAndEnabled)
            {
                continue;
            }

            IInteractable interactable = behaviour as IInteractable;
            if (interactable == null)
            {
                continue;
            }

            if (!interactable.CanInteract())
            {
                continue;
            }

            Transform target = interactable.GetInteractionTransform();
            if (target == null)
            {
                continue;
            }

            Vector3 targetPosition = target.position;
            if (interactable is NPC npc)
            {
                targetPosition = npc.GetInteractionPoint();
            }

            Vector3 toTarget = targetPosition - origin;
            float distance = toTarget.magnitude;
            if (distance <= 0.0001f)
            {
                distance = 0.0001f;
            }

            float maxDistance = interactable.GetMaxDistance();
            if (maxDistance <= 0f)
            {
                maxDistance = fallbackMaxDistance;
            }

            if (distance > maxDistance)
            {
                continue;
            }

            float dot = Vector3.Dot(forward, toTarget / distance);
            if (dot < viewDotThreshold)
            {
                continue;
            }

            // Prefer the object at the center of view first, then distance.
            float score = dot * 100f - distance;
            if (score > bestScore)
            {
                bestScore = score;
                best = interactable;
            }
        }

        return best;
    }
}
