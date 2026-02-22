using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueView : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Text speakerText;
    [SerializeField] private Text bodyText;
    [SerializeField] private Text continueHintText;
    [SerializeField] private RectTransform choicesContainer;

    private readonly List<Button> runtimeButtons = new List<Button>();
    private Font cachedFont;

    private void Awake()
    {
        EnsureView();
        SetVisible(false);
    }

    public void SetVisible(bool visible)
    {
        if (panel != null)
        {
            panel.SetActive(visible);
        }
    }

    public void ShowLine(string speaker, string line, bool showContinueHint)
    {
        EnsureView();
        SetVisible(true);

        speakerText.text = speaker;
        bodyText.text = line;

        continueHintText.gameObject.SetActive(showContinueHint);
        if (showContinueHint)
        {
            continueHintText.text = "F: Continue";
        }
    }

    public void ShowChoices(IList<DialogueManager.DialogueOption> options, Action<DialogueManager.DialogueOption> onSelected)
    {
        EnsureView();
        ClearChoices();

        if (options == null || options.Count == 0)
        {
            return;
        }

        continueHintText.gameObject.SetActive(false);

        for (int i = 0; i < options.Count; i++)
        {
            DialogueManager.DialogueOption selectedOption = options[i];
            Button button = CreateChoiceButton(selectedOption.Label);
            runtimeButtons.Add(button);

            button.onClick.AddListener(() => onSelected?.Invoke(selectedOption));
        }
    }

    public void ClearChoices()
    {
        for (int i = 0; i < runtimeButtons.Count; i++)
        {
            if (runtimeButtons[i] != null)
            {
                Destroy(runtimeButtons[i].gameObject);
            }
        }

        runtimeButtons.Clear();
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
            panel = new GameObject("DialoguePanel", typeof(RectTransform), typeof(Image), typeof(Outline));
            panel.transform.SetParent(transform, false);
        }

        RectTransform panelTransform = panel.GetComponent<RectTransform>();
        Image panelImage = panel.GetComponent<Image>();
        if (panelImage == null)
        {
            panelImage = panel.AddComponent<Image>();
        }
        panelImage.color = new Color(0f, 0f, 0f, 0.82f);

        Outline outline = panel.GetComponent<Outline>();
        if (outline == null)
        {
            outline = panel.AddComponent<Outline>();
        }
        outline.effectColor = new Color(1f, 1f, 1f, 0.15f);
        outline.effectDistance = new Vector2(1f, -1f);
        panelTransform.anchorMin = new Vector2(0.06f, 0.04f);
        panelTransform.anchorMax = new Vector2(0.94f, 0.62f);
        panelTransform.offsetMin = Vector2.zero;
        panelTransform.offsetMax = Vector2.zero;

        if (speakerText == null)
        {
            speakerText = CreateText(
                "SpeakerText",
                panelTransform,
                new Vector2(0.03f, 0.82f),
                new Vector2(0.97f, 0.97f),
                56,
                FontStyle.Bold,
                TextAnchor.MiddleLeft);
        }
        speakerText.font = cachedFont;
        speakerText.fontSize = 56;
        speakerText.fontStyle = FontStyle.Bold;
        speakerText.alignment = TextAnchor.MiddleLeft;
        ApplyAnchors(speakerText.rectTransform, new Vector2(0.03f, 0.82f), new Vector2(0.97f, 0.97f));

        if (bodyText == null)
        {
            bodyText = CreateText(
                "BodyText",
                panelTransform,
                new Vector2(0.03f, 0.38f),
                new Vector2(0.97f, 0.78f),
                40,
                FontStyle.Normal,
                TextAnchor.UpperLeft);
        }
        bodyText.font = cachedFont;
        bodyText.fontSize = 40;
        bodyText.fontStyle = FontStyle.Normal;
        bodyText.alignment = TextAnchor.UpperLeft;
        ApplyAnchors(bodyText.rectTransform, new Vector2(0.03f, 0.38f), new Vector2(0.97f, 0.78f));

        if (continueHintText == null)
        {
            continueHintText = CreateText(
                "ContinueHintText",
                panelTransform,
                new Vector2(0.72f, 0.06f),
                new Vector2(0.97f, 0.20f),
                32,
                FontStyle.Italic,
                TextAnchor.MiddleRight);
        }
        continueHintText.font = cachedFont;
        continueHintText.fontSize = 32;
        continueHintText.fontStyle = FontStyle.Italic;
        continueHintText.alignment = TextAnchor.MiddleRight;
        continueHintText.color = new Color(0.9f, 0.9f, 0.9f, 0.95f);
        ApplyAnchors(continueHintText.rectTransform, new Vector2(0.70f, 0.06f), new Vector2(0.97f, 0.20f));

        if (choicesContainer == null)
        {
            GameObject containerObject = new GameObject("Choices", typeof(RectTransform), typeof(HorizontalLayoutGroup));
            containerObject.transform.SetParent(panelTransform, false);
            choicesContainer = containerObject.GetComponent<RectTransform>();
        }

        ApplyAnchors(choicesContainer, new Vector2(0.03f, 0.08f), new Vector2(0.97f, 0.34f));

        HorizontalLayoutGroup layout = choicesContainer.GetComponent<HorizontalLayoutGroup>();
        if (layout == null)
        {
            layout = choicesContainer.gameObject.AddComponent<HorizontalLayoutGroup>();
        }
        layout.spacing = 18f;
        layout.padding = new RectOffset(0, 0, 4, 4);
        layout.childAlignment = TextAnchor.MiddleCenter;
        layout.childControlWidth = true;
        layout.childControlHeight = true;
        layout.childForceExpandWidth = true;
        layout.childForceExpandHeight = false;
    }

    private Button CreateChoiceButton(string label)
    {
        GameObject buttonObject = new GameObject("ChoiceButton", typeof(RectTransform), typeof(Image), typeof(Button), typeof(LayoutElement));
        buttonObject.transform.SetParent(choicesContainer, false);

        Image background = buttonObject.GetComponent<Image>();
        background.color = new Color(0.96f, 0.96f, 0.97f, 0.95f);

        LayoutElement layout = buttonObject.GetComponent<LayoutElement>();
        layout.minWidth = 0f;
        layout.flexibleWidth = 1f;
        layout.preferredHeight = 124f;

        GameObject textObject = new GameObject("Text", typeof(RectTransform), typeof(Text));
        textObject.transform.SetParent(buttonObject.transform, false);

        Text text = textObject.GetComponent<Text>();
        text.font = cachedFont;
        text.fontSize = 34;
        text.alignment = TextAnchor.MiddleCenter;
        text.color = new Color(0.08f, 0.08f, 0.08f, 1f);
        text.text = label;
        text.horizontalOverflow = HorizontalWrapMode.Wrap;
        text.verticalOverflow = VerticalWrapMode.Truncate;

        RectTransform textRect = textObject.GetComponent<RectTransform>();
        textRect.anchorMin = new Vector2(0.04f, 0.10f);
        textRect.anchorMax = new Vector2(0.96f, 0.90f);
        textRect.offsetMin = Vector2.zero;
        textRect.offsetMax = Vector2.zero;

        return buttonObject.GetComponent<Button>();
    }

    private Text CreateText(
        string name,
        RectTransform parent,
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
        text.color = Color.white;
        text.horizontalOverflow = HorizontalWrapMode.Wrap;
        text.verticalOverflow = VerticalWrapMode.Overflow;

        RectTransform rect = textObject.GetComponent<RectTransform>();
        rect.anchorMin = anchorMin;
        rect.anchorMax = anchorMax;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;

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
