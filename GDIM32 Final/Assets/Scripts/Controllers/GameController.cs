using System;
using UnityEngine;

public enum TrainingStage
{
    NotStarted,
    FeedAssigned,
    FeedCompleted,
    PlayAssigned,
    PlayCompleted,
    TrainingCompleted
}

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    [SerializeField] private Player player;
    [SerializeField] private Cat cat;

    public Player Player => player;
    public Cat Cat => cat;

    public TrainingStage CurrentTrainingStage { get; private set; } = TrainingStage.NotStarted;
    public bool HasCatFood { get; private set; }
    public bool HasCatToy { get; private set; }

    public event Action<TrainingStage> OnTrainingStageChanged;
    public event Action OnInventoryChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        ResolveSceneReferences();
    }

    private void Start()
    {
        ResolveSceneReferences();
        EnsureQuestItemsExist();

        OnTrainingStageChanged?.Invoke(CurrentTrainingStage);
        OnInventoryChanged?.Invoke();
    }

    private void ResolveSceneReferences()
    {
        if (player == null)
        {
            GameObject playerObj = GameObject.FindWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.GetComponent<Player>();
            }

            if (player == null)
            {
                player = FindObjectOfType<Player>();
            }
        }

        if (cat == null)
        {
            cat = FindObjectOfType<Cat>(true);
        }
    }

    public void AssignFeedQuest()
    {
        if (CurrentTrainingStage == TrainingStage.NotStarted)
        {
            SetTrainingStage(TrainingStage.FeedAssigned);
        }
    }

    public void CompleteFeedQuest()
    {
        if (CurrentTrainingStage == TrainingStage.FeedAssigned)
        {
            SetHasCatFood(false);
            SetTrainingStage(TrainingStage.FeedCompleted);
        }
    }

    public void AssignPlayQuest()
    {
        if (CurrentTrainingStage == TrainingStage.FeedCompleted)
        {
            SetTrainingStage(TrainingStage.PlayAssigned);
        }
    }

    public void CompletePlayQuest()
    {
        if (CurrentTrainingStage == TrainingStage.PlayAssigned)
        {
            SetHasCatToy(false);
            SetTrainingStage(TrainingStage.PlayCompleted);
        }
    }

    public void MarkTrainingCompleted()
    {
        if (CurrentTrainingStage == TrainingStage.PlayCompleted)
        {
            SetTrainingStage(TrainingStage.TrainingCompleted);
        }
    }

    public void SetHasCatFood(bool value)
    {
        if (HasCatFood == value)
        {
            return;
        }

        HasCatFood = value;
        OnInventoryChanged?.Invoke();
    }

    public void SetHasCatToy(bool value)
    {
        if (HasCatToy == value)
        {
            return;
        }

        HasCatToy = value;
        OnInventoryChanged?.Invoke();
    }

    private void SetTrainingStage(TrainingStage nextStage)
    {
        if (CurrentTrainingStage == nextStage)
        {
            return;
        }

        CurrentTrainingStage = nextStage;
        OnTrainingStageChanged?.Invoke(CurrentTrainingStage);
    }

    private void EnsureQuestItemsExist()
    {
        if (FindObjectOfType<CatFood>(true) == null)
        {
            CreatePlaceholderItem(isFood: true);
        }

        if (FindObjectOfType<CatToy>(true) == null)
        {
            CreatePlaceholderItem(isFood: false);
        }
    }

    private void CreatePlaceholderItem(bool isFood)
    {
        Vector3 anchor = cat != null ? cat.transform.position : Vector3.zero;
        Vector3 offset = isFood ? new Vector3(1.2f, 0.25f, 0f) : new Vector3(-1.2f, 0.25f, 0f);
        PrimitiveType primitive = isFood ? PrimitiveType.Cube : PrimitiveType.Sphere;

        GameObject itemObject = GameObject.CreatePrimitive(primitive);
        itemObject.name = isFood ? "Auto_CatFood" : "Auto_CatToy";
        itemObject.transform.position = anchor + offset;
        itemObject.transform.localScale = isFood ? new Vector3(0.35f, 0.2f, 0.35f) : new Vector3(0.28f, 0.28f, 0.28f);

        Renderer renderer = itemObject.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = isFood ? new Color(0.95f, 0.75f, 0.35f) : new Color(0.35f, 0.8f, 0.95f);
        }

        if (isFood)
        {
            itemObject.AddComponent<CatFood>();
        }
        else
        {
            itemObject.AddComponent<CatToy>();
        }

    }
}
