using UnityEngine;

public abstract class Item : MonoBehaviour, IInteractable
{
    [SerializeField] private string promptText = "F: Interact";
    [SerializeField] private float maxInteractDistance = 3f;

    private Collider[] cachedColliders;
    private Renderer[] cachedRenderers;
    private bool isVisible = true;
    private bool isSubscribed;

    protected virtual void Awake()
    {
        cachedColliders = GetComponentsInChildren<Collider>(true);
        cachedRenderers = GetComponentsInChildren<Renderer>(true);
    }

    protected virtual void OnEnable()
    {
        TrySubscribeToGameController();
        RefreshVisibility();
    }

    protected virtual void Start()
    {
        TrySubscribeToGameController();
        RefreshVisibility();
    }

    protected virtual void OnDisable()
    {
        if (GameController.Instance != null && isSubscribed)
        {
            GameController.Instance.OnTrainingStageChanged -= HandleGameStateChanged;
            GameController.Instance.OnInventoryChanged -= HandleGameStateChanged;
        }

        isSubscribed = false;
    }

    public virtual string GetPromptText()
    {
        return promptText;
    }

    public Transform GetInteractionTransform()
    {
        return transform;
    }

    public float GetMaxDistance()
    {
        return maxInteractDistance;
    }

    public bool CanInteract()
    {
        return enabled && gameObject.activeInHierarchy && isVisible;
    }

    public virtual void Interact()
    {
        if (!CanInteract())
        {
            return;
        }

        GameController controller = GameController.Instance;
        if (controller == null)
        {
            return;
        }

        OnInteract(controller);
        RefreshVisibility();
    }

    protected abstract bool ShouldBeVisible(GameController controller);
    protected abstract void OnInteract(GameController controller);

    private void HandleGameStateChanged()
    {
        RefreshVisibility();
    }

    private void HandleGameStateChanged(TrainingStage _)
    {
        RefreshVisibility();
    }

    protected void RefreshVisibility()
    {
        GameController controller = GameController.Instance;
        bool shouldShow = controller != null && ShouldBeVisible(controller);

        isVisible = shouldShow;

        for (int i = 0; i < cachedColliders.Length; i++)
        {
            if (cachedColliders[i] != null)
            {
                cachedColliders[i].enabled = shouldShow;
            }
        }

        for (int i = 0; i < cachedRenderers.Length; i++)
        {
            if (cachedRenderers[i] != null)
            {
                cachedRenderers[i].enabled = shouldShow;
            }
        }
    }

    private void TrySubscribeToGameController()
    {
        if (isSubscribed || GameController.Instance == null)
        {
            return;
        }

        GameController.Instance.OnTrainingStageChanged += HandleGameStateChanged;
        GameController.Instance.OnInventoryChanged += HandleGameStateChanged;
        isSubscribed = true;
    }
}
