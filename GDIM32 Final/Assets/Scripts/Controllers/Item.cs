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

    private GameController GetController()
    {
        if (GameController.Instance != null)
        {
            return GameController.Instance;
        }

        GameController controller = FindObjectOfType<GameController>(true);
        if (controller != null)
        {
            return controller;
        }

        GameController[] allControllers = Resources.FindObjectsOfTypeAll<GameController>();
        for (int i = 0; i < allControllers.Length; i++)
        {
            if (allControllers[i] != null && allControllers[i].gameObject.scene.isLoaded)
            {
                return allControllers[i];
            }
        }

        return null;
    }
    public virtual void Interact()
    {
        if (!CanInteract())
        {
            return;
        }

        GameController controller = GetController();
        if (controller == null)
        {
            Debug.Log("[Item] GameController is still null");
            return;
        }

        Debug.Log("[Item] Interact called on " + gameObject.name);

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

    public void RefreshVisibility()
    {
        GameController controller = GetController();
        if (controller == null)
        {
            Debug.Log("[Item] RefreshVisibility: GameController is still null");
            return;
        }

        bool shouldShow = ShouldBeVisible(controller);
        isVisible = shouldShow;

        if (cachedColliders == null)
        {
            cachedColliders = GetComponentsInChildren<Collider>(true);
        }

        if (cachedRenderers == null)
        {
            cachedRenderers = GetComponentsInChildren<Renderer>(true);
        }

        foreach (Collider col in cachedColliders)
        {
            if (col != null)
            {
                col.enabled = shouldShow;
            }
        }

        foreach (Renderer rend in cachedRenderers)
        {
            if (rend != null)
            {
                rend.enabled = shouldShow;
            }
        }
    }

    private void TrySubscribeToGameController()
    {
        GameController controller = GetController();

        if (isSubscribed || controller == null)
        {
            return;
        }

        controller.OnTrainingStageChanged += HandleGameStateChanged;
        controller.OnInventoryChanged += HandleGameStateChanged;
        isSubscribed = true;
    }
}
