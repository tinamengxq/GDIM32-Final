using UnityEngine;

public class Cat : NPC
{
    [SerializeField] private Animator catAnimator;
    [SerializeField] private AudioSource catAudioSource;
    [SerializeField] private string eatStateName = "Armature|eat";
    [SerializeField] private string playStateName = "Armature|play";
    [SerializeField] private GameObject catFoodPrefab;
    [SerializeField] private GameObject catToyPrefab;
    [SerializeField] private Transform eatingPosition;
    [SerializeField] private Transform playingPosition;

    private GameObject food;
    private GameObject toy;
    

    private void Awake()
    {
        if (catAnimator == null)
        {
            catAnimator = GetComponentInChildren<Animator>(true);
        }

        if (catAudioSource == null)
        {
            catAudioSource = GetComponent<AudioSource>();
        }
    }

    private void Start()
    {
        if (catAnimator != null)
        {
            catAnimator.enabled = true;
        }
    }

    public override bool CanInteract()
    {
        if (!base.CanInteract())
        {
            return false;
        }

        GameController controller = GameController.Instance;
        if (controller == null)
        {
            return false;
        }

        if (controller.CurrentTrainingStage == TrainingStage.FeedAssigned)
        {
            return controller.HasCatFood;
        }

        if (controller.CurrentTrainingStage == TrainingStage.PlayAssigned)
        {
            return controller.HasCatToy;
        }

        return false;
    }

    public override void Interact()
    {
        GameController controller = GameController.Instance;
        if (controller == null)
        {
            return;
        }

        if (controller.CurrentTrainingStage == TrainingStage.FeedAssigned)
        {
            if (!controller.HasCatFood)
            {
                return;
            }
            
            MakeFoodAppear();
            PlayCatAnimation(eatStateName);
            if (catAudioSource != null)
            {
                catAudioSource.Play();
            }
            controller.SetHasCatFood(false);
            controller.CompleteFeedQuest();
            return;
        }

        if (controller.CurrentTrainingStage == TrainingStage.PlayAssigned)
        {
            if (!controller.HasCatToy)
            {
                return;
            }
            MakeToyAppear();
            PlayCatAnimation(playStateName);
            controller.CompletePlayQuest();
        }
    }
    private void MakeFoodAppear()
    {
        food = Instantiate(catFoodPrefab, eatingPosition.position, eatingPosition.rotation);
        Destroy(food, 10f);
    }

    private void MakeToyAppear()
    {
        toy = Instantiate(catToyPrefab, playingPosition.position, playingPosition.rotation);
        Destroy(toy, 10f); 
    }

    private void PlayCatAnimation(string stateName)
    {
        if (catAnimator == null || string.IsNullOrWhiteSpace(stateName))
        {
            return;
        }

        catAnimator.enabled = true;
        catAnimator.Play(stateName, 0, 0f);
    }
}
