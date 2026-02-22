using UnityEngine;
using UnityEngine.UI;

public class InteractPromptView : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Text promptText;

    private Font cachedFont;

    private void Awake()
    {
        EnsureView();
        Hide();
    }

    public void Show(string prompt)
    {
        EnsureView();
        panel.SetActive(true);
        promptText.text = string.IsNullOrWhiteSpace(prompt) ? "F: Interact" : prompt;
    }

    public void Hide()
    {
        EnsureView();
        panel.SetActive(false);
    }

    private void EnsureView()
    {
        RectTransform rootRect = GetComponent<RectTransform>();
        if (rootRect == null)
        {
            rootRect = gameObject.AddComponent<RectTransform>();
        }

        rootRect.anchorMin = Vector2.zero;
        rootRect.anchorMax = Vector2.one;
        rootRect.offsetMin = Vector2.zero;
        rootRect.offsetMax = Vector2.zero;

        DisableLegacyTMPIfPresent();

        if (cachedFont == null)
        {
            cachedFont = LegacyFontProvider.GetFont();
        }

        if (panel == null)
        {
            panel = new GameObject("InteractPanel", typeof(RectTransform), typeof(Image));
            panel.transform.SetParent(transform, false);

            Image panelImage = panel.GetComponent<Image>();
            panelImage.color = new Color(0f, 0f, 0f, 0.68f);

            RectTransform panelRect = panel.GetComponent<RectTransform>();
            panelRect.anchorMin = new Vector2(0.44f, 0.46f);
            panelRect.anchorMax = new Vector2(0.56f, 0.53f);
            panelRect.offsetMin = Vector2.zero;
            panelRect.offsetMax = Vector2.zero;
        }

        if (promptText == null)
        {
            GameObject textObject = new GameObject("PromptText", typeof(RectTransform), typeof(Text));
            textObject.transform.SetParent(panel.transform, false);

            promptText = textObject.GetComponent<Text>();
            promptText.font = cachedFont;
            promptText.fontSize = 22;
            promptText.alignment = TextAnchor.MiddleCenter;
            promptText.color = Color.white;

            RectTransform textRect = textObject.GetComponent<RectTransform>();
            textRect.anchorMin = Vector2.zero;
            textRect.anchorMax = Vector2.one;
            textRect.offsetMin = Vector2.zero;
            textRect.offsetMax = Vector2.zero;
        }
    }

    private void DisableLegacyTMPIfPresent()
    {
        Component textMeshPro = GetComponent("TextMeshProUGUI");
        if (textMeshPro is Behaviour behaviour)
        {
            behaviour.enabled = false;
        }
    }
}
