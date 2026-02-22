using UnityEngine;

public class UI : MonoBehaviour
{
    public static UI Instance { get; private set; }

    [SerializeField] private RectTransform runtimeCanvasRoot;
    [SerializeField] private DialogueView dialogueView;
    [SerializeField] private QuestView questView;
    [SerializeField] private InteractPromptView interactPromptView;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        EnsureCanvas();
        EnsureViews();
        DisableLegacySceneUI();
    }

    private void Start()
    {
        if (GameController.Instance != null)
        {
            GameController.Instance.OnTrainingStageChanged += HandleTrainingStageChanged;
            HandleTrainingStageChanged(GameController.Instance.CurrentTrainingStage);
        }

        HideInteractPrompt();
    }

    private void OnDestroy()
    {
        if (GameController.Instance != null)
        {
            GameController.Instance.OnTrainingStageChanged -= HandleTrainingStageChanged;
        }
    }

    public DialogueView DialogueView => dialogueView;
    public QuestView QuestView => questView;
    public InteractPromptView InteractPromptView => interactPromptView;

    public void ShowInteractPrompt(string prompt)
    {
        if (interactPromptView != null)
        {
            interactPromptView.Show(prompt);
        }
    }

    public void HideInteractPrompt()
    {
        if (interactPromptView != null)
        {
            interactPromptView.Hide();
        }
    }

    public void ProgressUI(int waitingTime)
    {
    }

    private void HandleTrainingStageChanged(TrainingStage stage)
    {
        if (questView != null)
        {
            questView.Refresh(stage);
        }
    }

    private void EnsureCanvas()
    {
        if (runtimeCanvasRoot != null)
        {
            return;
        }

        Transform existingCanvas = transform.Find("RuntimeCanvas");
        if (existingCanvas is RectTransform existingRect && existingCanvas.GetComponent<Canvas>() != null)
        {
            runtimeCanvasRoot = existingRect;
            return;
        }

        GameObject canvasObject = new GameObject(
            "RuntimeCanvas",
            typeof(RectTransform),
            typeof(Canvas),
            typeof(UnityEngine.UI.CanvasScaler),
            typeof(UnityEngine.UI.GraphicRaycaster));

        runtimeCanvasRoot = canvasObject.GetComponent<RectTransform>();
        runtimeCanvasRoot.SetParent(transform, false);
        runtimeCanvasRoot.anchorMin = Vector2.zero;
        runtimeCanvasRoot.anchorMax = Vector2.one;
        runtimeCanvasRoot.offsetMin = Vector2.zero;
        runtimeCanvasRoot.offsetMax = Vector2.zero;

        Canvas canvas = canvasObject.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        UnityEngine.UI.CanvasScaler scaler = canvasObject.GetComponent<UnityEngine.UI.CanvasScaler>();
        scaler.uiScaleMode = UnityEngine.UI.CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920f, 1080f);
        scaler.screenMatchMode = UnityEngine.UI.CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        scaler.matchWidthOrHeight = 0.5f;
    }

    private void EnsureViews()
    {
        RectTransform dialogueRoot = GetOrCreateChild(runtimeCanvasRoot, "Dialogue UI");
        RectTransform interactRoot = GetOrCreateChild(runtimeCanvasRoot, "Interaction UI");
        RectTransform questRoot = GetOrCreateChild(runtimeCanvasRoot, "Quest UI");

        if (dialogueView == null)
        {
            dialogueView = dialogueRoot.GetComponent<DialogueView>();
            if (dialogueView == null)
            {
                dialogueView = dialogueRoot.gameObject.AddComponent<DialogueView>();
            }
        }

        if (interactPromptView == null)
        {
            interactPromptView = interactRoot.GetComponent<InteractPromptView>();
            if (interactPromptView == null)
            {
                interactPromptView = interactRoot.gameObject.AddComponent<InteractPromptView>();
            }
        }

        if (questView == null)
        {
            questView = questRoot.GetComponent<QuestView>();
            if (questView == null)
            {
                questView = questRoot.gameObject.AddComponent<QuestView>();
            }
        }
    }

    private RectTransform GetOrCreateChild(RectTransform parent, string childName)
    {
        Transform child = parent.Find(childName);
        if (child is RectTransform childRect)
        {
            return childRect;
        }

        GameObject childObject = new GameObject(childName, typeof(RectTransform));
        RectTransform rect = childObject.GetComponent<RectTransform>();
        rect.SetParent(parent, false);
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;
        return rect;
    }

    private void DisableLegacySceneUI()
    {
        string[] legacyNames = { "Dialogue UI", "Interaction UI", "Quest UI", "Progress UI" };

        for (int i = 0; i < legacyNames.Length; i++)
        {
            Transform legacy = transform.Find(legacyNames[i]);
            if (legacy != null && legacy != runtimeCanvasRoot)
            {
                legacy.gameObject.SetActive(false);
            }
        }
    }
}
