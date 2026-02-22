using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class QuestView : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Text titleText;
    [SerializeField] private Text questText;

    private Font cachedFont;

    private void Awake()
    {
        EnsureView();
        panel.SetActive(false);
    }

    public void Refresh(TrainingStage stage)
    {
        EnsureView();

        bool showQuest = stage != TrainingStage.NotStarted;
        panel.SetActive(showQuest);
        if (!showQuest)
        {
            return;
        }

        bool feedDone = stage >= TrainingStage.FeedCompleted;
        bool playAvailable = stage >= TrainingStage.PlayAssigned;
        bool playDone = stage >= TrainingStage.PlayCompleted;

        StringBuilder builder = new StringBuilder();
        builder.AppendLine($"{GetCheckPrefix(feedDone)} Feed the Cat");
        if (playAvailable || playDone || stage == TrainingStage.TrainingCompleted)
        {
            builder.AppendLine($"{GetCheckPrefix(playDone)} Play with the Cat");
        }

        if (stage == TrainingStage.TrainingCompleted)
        {
            builder.AppendLine($"{GetCheckPrefix(true)} Training Completed");
        }

        titleText.text = "Quest List";
        questText.text = builder.ToString().TrimEnd();
    }

    private static string GetCheckPrefix(bool completed)
    {
        return completed
            ? "[<color=#95FFC8>\u2713</color>]"
            : "[<color=#D6E3F3>\u25CB</color>]";
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

        if (cachedFont == null)
        {
            cachedFont = LegacyFontProvider.GetFont();
        }

        if (panel == null)
        {
            panel = new GameObject("QuestPanel", typeof(RectTransform), typeof(Image), typeof(Outline));
            panel.transform.SetParent(transform, false);
        }

        Image panelImage = panel.GetComponent<Image>();
        if (panelImage == null)
        {
            panelImage = panel.AddComponent<Image>();
        }
        panelImage.color = new Color(0.07f, 0.13f, 0.23f, 0.82f);

        Outline outline = panel.GetComponent<Outline>();
        if (outline == null)
        {
            outline = panel.AddComponent<Outline>();
        }
        outline.effectColor = new Color(0.8f, 0.9f, 1f, 0.2f);
        outline.effectDistance = new Vector2(1f, -1f);

        RectTransform panelRect = panel.GetComponent<RectTransform>();
        panelRect.anchorMin = new Vector2(0.70f, 0.52f);
        panelRect.anchorMax = new Vector2(0.972f, 0.92f);
        panelRect.offsetMin = Vector2.zero;
        panelRect.offsetMax = Vector2.zero;

        if (titleText == null)
        {
            titleText = CreateText(
                "TitleText",
                panel.transform,
                new Vector2(0.08f, 0.82f),
                new Vector2(0.92f, 0.96f),
                42,
                FontStyle.Bold,
                TextAnchor.UpperLeft);
        }
        titleText.font = cachedFont;
        titleText.fontSize = 42;
        titleText.fontStyle = FontStyle.Bold;
        titleText.alignment = TextAnchor.UpperLeft;
        titleText.color = new Color(0.95f, 0.98f, 1f, 1f);
        ApplyAnchors(titleText.rectTransform, new Vector2(0.08f, 0.82f), new Vector2(0.92f, 0.96f));

        if (questText == null)
        {
            questText = CreateText(
                "QuestText",
                panel.transform,
                new Vector2(0.08f, 0.11f),
                new Vector2(0.92f, 0.80f),
                38,
                FontStyle.Normal,
                TextAnchor.UpperLeft);
        }

        questText.font = cachedFont;
        questText.fontSize = 38;
        questText.fontStyle = FontStyle.Normal;
        questText.alignment = TextAnchor.UpperLeft;
        questText.color = new Color(0.96f, 0.98f, 1f, 1f);
        questText.supportRichText = true;
        questText.lineSpacing = 1.12f;
        ApplyAnchors(questText.rectTransform, new Vector2(0.08f, 0.11f), new Vector2(0.92f, 0.80f));
    }

    private Text CreateText(
        string name,
        Transform parent,
        Vector2 anchorMin,
        Vector2 anchorMax,
        int fontSize,
        FontStyle fontStyle,
        TextAnchor alignment)
    {
        GameObject textObject = new GameObject(name, typeof(RectTransform), typeof(Text));
        textObject.transform.SetParent(parent, false);

        Text text = textObject.GetComponent<Text>();
        text.font = cachedFont;
        text.fontSize = fontSize;
        text.fontStyle = fontStyle;
        text.alignment = alignment;
        text.horizontalOverflow = HorizontalWrapMode.Wrap;
        text.verticalOverflow = VerticalWrapMode.Overflow;

        RectTransform textRect = textObject.GetComponent<RectTransform>();
        textRect.anchorMin = anchorMin;
        textRect.anchorMax = anchorMax;
        textRect.offsetMin = Vector2.zero;
        textRect.offsetMax = Vector2.zero;

        return text;
    }

    private static void ApplyAnchors(RectTransform rect, Vector2 anchorMin, Vector2 anchorMax)
    {
        if (rect == null)
        {
            return;
        }

        rect.anchorMin = anchorMin;
        rect.anchorMax = anchorMax;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;
    }
}
