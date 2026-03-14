# GDIM32-Final
## Check-In
### Team Devlog:
During development, we faced an issue with interaction targeting. When multiple interactable objects were present in the scene, the system would sometimes select an object that was within range but not clearly centered in the player’s view. This made interaction feel inconsistent and occasionally caused the wrong object to be triggered.
To solve this problem, I used **breakpoints** in Visual Studio to debug the interaction selection logic inside the `PlayerInteractor` script.
I chose breakpoints instead of the Unity Profiler or Gizmos because the issue was not related to performance or visual alignment, but a logic-based selection problem. I needed to inspect runtime variable values directly inside the targeting algorithm.

The interaction detection happens in the `FindBestInteractable()` method. The system iterates through all active IInteractable objects and evaluates them based on:

- Distance from the camera to the interaction point
- Direction vector from the camera to the object
- Dot product between `cameraTransform.forward` and the normalized direction vector
- A threshold value (viewDotThreshold)
- A scoring formula that prioritizes centered objects


By stepping through the code while multiple objects were visible on screen, I was able to observe the actual dot values, distance values, and final score comparisons in real time. I noticed that small differences in dot values significantly affected which object was selected, especially when two interactable objects were close to each other.


During debugging, I also discovered a second issue. Occasionally, when the player pressed the interaction key (F) while facing the Clerk, a different NPC such as the Cat would be triggered instead. At first, this appeared to be another scoring imbalance problem. However, after stepping through the loop with breakpoints, I realized that more interactable objects were being detected than were visibly present in the scene.

Although only two NPC characters appeared in the game world, there were actually four objects implementing the IInteractable interface. In some cases, the NPC interaction script had been attached both to the visible character model and to an empty parent GameObject. As a result, the targeting system was evaluating duplicate interaction sources for the same character. Because the scoring system selects the highest calculated score, the invisible or unintended object could sometimes win the comparison, leading to inconsistent interaction behavior.

After identifying this hierarchy issue, we reorganized the scene structure to ensure that each NPC had only one active interactable component attached to the correct object. This eliminated duplicate targets and made the interaction behavior consistent.


To further understand the impact of these variables, I experimented with adjusting the `viewDotThreshold` and observing how changes in the scoring weight influenced object selection behavior. Through this process, I confirmed that the interaction system was functioning as designed, but that small parameter differences could noticeably affect user perception of targeting accuracy. This helped me better understand the sensitivity of weighted scoring systems in gameplay mechanics.


**Reflection:**
This debugging process improved my understanding of how mathematical weighting directly affects player experience. Although the interaction system was logically correct, small imbalances in scoring weights produced unintuitive results during gameplay. Through step-by-step inspection in Visual Studio code using breakpoints, I learned the importance of validating gameplay systems not only through theoretical correctness but also through practical testing in realistic scenarios. In future projects, I would consider visualizing targeting data or implementing raycast-based prioritization earlier in development to reduce ambiguity in object selection.



### Tina Meng
Contribution:

1. I built one of the two light sources. 
2. I found, imported and created the scenes. 
    - Create and drew terrain
    - Drag prefabs in imported assets into the scene (link in open-source assets)
        - Environment (flowers, trees, rocks)
        - Town (houses, cargos...)
3. I imported and created the skybox. (link in open-source assets)
    - Based on the youtube link included in open-source assets
4. I coded
    - I forked the github repository and created unity project.
    - I created scripts that determiend basic coding structure of the project.
    - I coded in several scripts. 
        - I WAS THE FIRST TO EDIT THE PROJECT. AND ALL THE FOLLOWING METHODS WERE DELECTED BY OTHER GROUP MEMBERS BECAUSE THEY FOUND OTHER WAYS TO RUN THE CODES. THAT'S WHY YOU CAN'T FIND THESE THINGS IN CURRENT CODES. BUT YOU CAN STILL FIND THEM IN COMMITS.  
        - NPC
            - public virtual void Interaction()
            - void EnableDialogue()
            - protected virtual void Update()
            - protected virtual void Start()
        - Clerk
            - public override void Interaction()
        - Cat
            - public override void Interaction()
            - private void JustDoIt(string animatorString, CatState newState, int questNumber)
            - CatState enum
        - GameController
            - private void Awake()
            - created event NPCAction
            - created list "quests"
            - public void CompleteQuest(int questNumber)
        - Quest
            - QuestState enum
            - public void ListofQuests()
    - I established the base of dialogue
        - I WAS THE FIRST TO EDIT THE DIALOGUE. AND ANOTHER GROUP MEMBER CAME LATER TO GO ON DOING THE DIALOGUE STUFF. THE SCRIPTS AND UNITY WORK STILL APPEARS IN THE PROJECT, BUT THAT GROUP MEMBER DIDN'T FOLLOW MY WORK, INSTEAD SHE USED OTHER SCRIPTS TO CREATE DIALOGUE.
        - Dialogue Manager.cs
            - public void StartDialogue(Dialogue dialogue)
            - private void Awake()
        - Dialogue.cs
        - And related works in unity
    - I debugged the quest UI problem of the checking UI didn't appear in itch but works in unity.

Reflection: 
1. I wrote the break down for the game. And when I was editing the scripts, it worked pretty well. It was really helpful when I was construction the scripts.
2. I followed inheritance pattern in proposal for NPC and Items. It worked well and was helpful for my construction.
3. I used finite state machine pattern in proposal. It was helpful for my construction.
    - I used fsm for cat and quests. 
    - Group members deleted my fsm for cat and quests.
    - They wrote another fsm for quests.
4. I used singleton patterns in proposal for many controllers. It was helpful for me to reach codes directly.
5. We planed to import 3D assets for the town. But we didn't state clearly on which asset to import, which led to great disagreements between group members. We should state clearly next time in a game proposal on what kind of asset should we import. 


### Yuxin Ding
In this stage of the project, my main contributions focued on the player control system, specifically first-person camera control, and interaction detection. While other team members worked on dialogue, quest structure, and UI systems, I was mainly responsible for implementing and refining how the player moves, looks around and successfully interact with objects in the final build.<br><br>


**What I mainly did:**
1. I implemented and refined the first-person mouse look system in the Player script.
   This included:

   - Enabling full 360° horizontal rotation
   - Clamping vertical rotation between -90 and 90 degrees to prevent unnatural flipping
   - Adjusting the `lookSensitivity` variable to improve responsiveness
   - Tuning sensitivity values so the player does not need excessive mouse movement to reach a desired viewing angle

Through testing and iteration, I modified sensitivity values to achieve smoother and more natural camera control. At the beginning, the rotation felt too slow and required large mouse movement. These adjustments directly improved the gameplay experience.


2. I also worked on refining how interactable objects are detected and how interaction is triggered. I refined the interaction targeting system inside the PlayerInteractor component. The system evaluates all active IInteractable objects and selects the best target based on distance and directional alignment from the camera’s perspective.
   The detection logic calculates:

   - The distance from the camera to the target
   - The direction vector toward the interaction point
   - The dot product between cameraTransform.forward and the normalized direction to the target
  
Only objects within the maximum interaction distance and above a viewDotThreshold are considered valid targets.<br><br>
  
I adjusted parameters such as:

- viewDotThreshold
- Maximum interaction distance
- Detection conditions to ensure that pressing the F key reliably triggers interaction

Sometimes objects were not detected even when the player was looking at them. I tested different angle and distance values and adjusted them to make the interaction feel more natural and consistent. My goal was to ensure that the full interaction flow works smoothly: detection, prompt display, and successful triggering.


3. I found audio source for cat eating animation.<br><br>

   
**Additional contributions:**

In addition to gameplay systems, I slightly adjusted the Directional Light’s position and color to better differentiate it from the Spot Light.
I also collaborated with teammates in searching for environmental assets and contributed to a small portion of scene setup.

Besides coding, I often played the game during development to test interaction feel and identify issues. When I noticed detection problems or camera control discomfort, I reported them and adjusted the related values. Although everyone participated in playtesting, I personally focused more on identifying small usability issues, questioning them and together we found solutions out to fix them.

These contributions were supportive in nature, while my primary focus remained on player control and interaction functionality.<br><br>


**Reflection:**

Our original proposal outlined the general interaction concept quite clearly, but during development, I realized that implementing interaction systems requires careful tuning of numerical thresholds and detection logic. I realized that even a small change in the dot threshold significantly changed how strict the detection felt.

Small values such as camera sensitivity, interaction distance, and view-angle thresholds significantly impact player experience.

In the future project, I would plan player control tuning earlier in development if needed. And define technical interaction methods more clearly in the proposal.
I also learned that documenting value adjustments during testing is important for better tracking and team communication.


Overall, working on the player control and interaction system helped me understand how core gameplay feel depends heavily on precise implementation and iteration.



### Yan Zhang 
I mainly continue to add things on the basis of what others have built before. I first imported the cat's resource package, put the cat prefab into the scene, assigned it an Animator Controller, and then wrote code in Cat.cs to control the animation. Now I use Animator.Play(stateName) to switch different states. The cat itself is also an interactive object. It still uses the original NPC interaction logic, triggered by Interact(), so the whole system is connected to the original interaction structure.


Then I finished the logic of the cat-related item. The quest system is managed by GameController.cs. The quest state is controlled there. When the state changes, an event is triggered. UI.cs refreshes the quest panel when it receives the event. In this part, I mainly rationalized the logic, because although the variables were changing at the beginning, the UI was not updated, and it looked like there was no response. And I have completely connected the chain of "quest state change → trigger event → UI update".


On the side of the dialogue system, the previous team members probably set up a basic structure, but the actual logic was not finished. Instead of directly following her structure, I rearranged the process of triggering and advancing the dialogue, and made Interact() call the DialogueManager. After the conversation ends, it can also affect the quest state, so the dialogue system and the quest system are connected.


In addition, I also added global background music and got a resident AudioManager (Singleton) to control the background sound.


**Reflection:**

I think our plan is quite detailed. It is really useful when I first start writing code. Just follow it step by step. But in the middle of the period, I found the problem - the proposal was too idealistic and didn't think much about whether I could do it or not. Some designs look very good, but when you really write them, you will find that they are not so simple. In the end, many places can only be simplified or achieved in other ways.


When I do the project in the future, I will be more realistic at the proposal stage. First, I will find out how much I can do, which functions must be completed, and which can be added later.


## Final Submission
### Group Devlog
In the systam architechture section of our game proposal document, we picked "MVC", "Singleton", and "Interitance & Polymorphism" as our 3 design patterns. Actually, we included the 4th design pattern finite state machine in our proposal by implementing it in "MVC" and "Interitance & Polymorphism". We fully utilized these patterns in our project as I see them everytime I opened our scripts. 

1. MVC
- The Model-View-Controller is a design pattern that systems are decoupled from each other. Model stands for game data, view is visuals and results, and controller presents pure game logic. Inside the MVC pattern, controller stewards model, and view listens to controller. View subscribes to controller events and reacts to changes. 
- In our game, we used MVC for our training stage change. 
    - In GameController.cs, we created a finite state machine using enum called TraningStage. It separates different stages of the player before and after each quest is pendng assigned, assigned, pending finished, and finished. 
    ```
    public enum TrainingStage
    {
    NotStarted,
    FeedAssigned,
    FeedCompleted,
    PlayAssigned,
    PlayCompleted,
    TrainingCompleted
    }
    ```
    - Then, we created events as "C" in "MVC" to stewards the player's stage updates. We first set training stage as training not started, and then we named an event called OnTrainingStageChanged. 
    ```
    public TrainingStage CurrentTrainingStage { get; private set; } = TrainingStage.NotStarted;
    public event Action<TrainingStage> OnTrainingStageChanged;
    ```
    - Once started, the quests are pending to be assigned, the controller starts to focus on whether the training status have changed. If the dialogue of assigning the first quest has played between the clerk NPC and the player, the controller will know that the state of training is changed. At this moment, the event of OnTrainingStageChanged will be called in a method called SetTrainingStage. 
    ```
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
    ```
    - Inside the method called SetTrainingStage(), the event is invoked. This is how the controller stewards the model. 
    ```
    public void SetTrainingStage(TrainingStage nextStage)
    {
        ...
        OnTrainingStageChanged?.Invoke(CurrentTrainingStage);
        ...
    }
    ```
    - If the game data shows changes, controller will know the changes as the event is called. The event is then linked to the view. Once the event is called, the methods subscribed to the event inside the UI script will responde immediately to lead to changes in the view part shown to the players. Visually, this will be presented as checking the quest in the questlist on the top-right corner on the screen. Thus, in the UI script, a method called HandleTrainingStateChanged will be subscribed to OnTrainingStageChanged in the Start method.
    ```
    private void Start()
    {
        ...
        GameController.Instance.OnTrainingStageChanged += HandleTrainingStageChanged;
        HandleTrainingStageChanged(GameController.Instance.CurrentTrainingStage);
        ...
    }

    private void HandleTrainingStageChanged(TrainingStage stage)
    {
        ...
        questView.Refresh(stage);
        ...
    }
    ```
    - And the Refresh method here locates in the script called QuestView. This script controlls how quests appear on the top-right corner of the screen and how checking marks appear next to the content of the quest.
    - To sum up, MVC is used to control a chain of changes caused by the update of quest states. In gamecontroller, it stewards the game data of trainingstates to see when the event should be called. When the event is called, it calls methods in UI scripts to show changes in trainingstates in the ui view. 
- Similarly, we also used MVC for the items. 
    - Again, in the game controller, we created an event called OnInventaryChanged. This event will steward the game data of whether the player has item with them. The items refer to the props used in the trainings: the food to feed the cat, and the toy to play with the cat. 
    ```
    public event Action OnInventoryChanged;
    public bool HasCatFood { get; private set; }
    public bool HasCatToy { get; private set; }

    public void SetHasCatFood(bool value)
    {
        if (HasCatFood == value)
        {
            return;
        }

        HasCatFood = value;
        OnInventoryChanged?.Invoke();

        Item[] items = FindObjectsOfType<Item>(true);
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null)
            {
                items[i].RefreshVisibility();
            }
        }
    }

    public void SetHasCatToy(bool value)
    {
        if (HasCatToy == value)
        {
            return;
        }

        HasCatToy = value;
        OnInventoryChanged?.Invoke();

        Item[] items = FindObjectsOfType<Item>(true);
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null)
            {
                items[i].RefreshVisibility();
            }
        }
    }
    ```
    - The two methods that calls the event appears in scripts of CatFood and CatToy. If the player interacts with the prop, the game data will know that the player interacted with it, then controller will see player interacted with the prop and call the event to change the view so that the prop will no longer appear in the place where they are placed to wait for the playr to interact.
    ```
    // CatToy : Item
    protected override void OnInteract(GameController controller)
    {
        controller.SetHasCatToy(true);
        Debug.Log("[CatToy] Picked up cat toy.");
    }

    // CatFood : Item
    protected override void OnInteract(GameController controller)
    {
        ...
        controller.SetHasCatFood(true);
        Debug.Log("[CatFood] Picked up cat food.");
        ...
    }
    ```
    - Then what is subscribed to the event? In item script, some strange things happened to make a method HandleGameStateChanged to subscribe to the event. But the subscription finally appeared to take place in the Start method. Because the entire process of subscription is too frustrating, I will only show the line of code for subscription.
    ```
    controller.OnInventoryChanged += HandleGameStateChanged;

    private void HandleGameStateChanged()
    {
        RefreshVisibility();
    }
    ```
    - And so the method of refreshvisibility is very long and I will not copy it here. This method changes whether the prop will appear in the scene. 
    - To sum up, the MVC pattern used for the items in our game is much simpler than that used for our training states because the number of items is much smaller than the number of training states. M stands for whether player has interacted with the item. If the controller finds that player has interacted with the item, it will call the event to change visual appearance of the prop. When the event is called, whether the item appears in the scene will be refreshed, the players will then find out that, by watching the scene, they actually picked the item from the ground.







2. Singleton
3. Inheritance & Polymorphism


### Yuxin Ding
Between the period of check-in and the final submission of the project, my main contributions focused on implementing additional UI such as the start screen and the in-game welcome/hint page. Also, I contributed to the debugging and optimization of the whole dialogue system.
My goal was to make sure the quest progression worked correctly across multiple stages and that the player receives clear feedback when entering the game.


**Dialogue and Quest System Improvements:**

One of the systems I worked on was improving the interaction flow between the Clerk NPC, the DialogueManager, the GameController, and other scripts related to the dialogue process. When Scriptable Objects were introduced to manage dialogue nodes, mismatches between the code and node configuration in Unity caused the dialogue system to stop functioning correctly. So I took over the debugging and modification process from the team members. The Scriptable Objects were already set up, but the dialogue system had crashed. After team member identified and fixed the problem where the content of the conversation was linked to the wrong option and different dialogue options could not be triggered (this is how I understood the issue), I started fixing the rest of the problems. I focused on debugging this system and stabilizing the dialogue interactions so that the quest logic and dialogue options could work together properly.


During testing, I discovered that the player could not reliably receive the training quest from the Clerk NPC. When selecting the training option in the dialogue menu, the quest assignment did not always trigger correctly. To investigate this issue, I reviewed how dialogue interactions communicate with the quest system managed by the **GameController**.


The quest progression relies on the variable `CurrentTrainingStage`, which tracks the player’s progress through the training storyline. The **GameController** manages quest progression through methods such as `AssignFeedQuest()`, `CompleteFeedQuest()`, `AssignPlayQuest()`, and `CompletePlayQuest()`. I tested the interaction flow between the **Clerk NPC**, the **DialogueManager**, and the **GameController** to verify that the correct quest assignment method was triggered when the player selected the training option. After adjusting the quest assignment logic and confirming that the correct values were passed to `CurrentTrainingStage`, the feeding quest could be accepted reliably.


After the quest could be received properly, I encountered additional issues during the interaction stage. Even when the feeding quest was active, the player sometimes could not complete the interaction with the cat. For example, pressing **F** did not always trigger the interaction, the **food bowl** object sometimes failed to appear, or the quest did not update to **completed** after the interaction.


To resolve these problems, I reviewed how the quest state interacts with the item system. Quest items such as **CatFood** and **CatToy** appear depending on the current training stage and rely on the `RefreshVisibility()` method implemented in the **Item** class. I verified that updates to `CurrentTrainingStage` triggered the `OnTrainingStageChanged` event and that this event correctly called `RefreshVisibility()` to update item visibility. After adjusting this logic, the quest items appeared at the correct stages and the feeding interaction could be completed successfully.


After the main interaction flow became functional, I continued testing and found additional issues related to dialogue options. The **Cat Knowledge** option in the dialogue menu could not be selected during the early stage of the quest, preventing access to that dialogue path. After reviewing the dialogue node connections and adjusting the configuration, this option became selectable again.


However, resolving this issue revealed another problem: in some cases the next stage of the training quest would fail to trigger after the feeding quest was completed. Because the dialogue system, quest logic, and item visibility system are closely connected, fixing one issue sometimes exposed inconsistencies in other parts of the system. I therefore continued adjusting the dialogue node connections and quest stage logic while repeatedly testing the full quest progression.


After these debugging steps, the entire training quest sequence works consistently. The player can interact with the Clerk NPC, receive the feeding quest, complete the feeding interaction with the cat, unlock the next stage of the training quest, and progress through the storyline without the dialogue system breaking or the quest chain resetting unexpectedly.<br><br>


**Start UI and Welcome Interface**

In addition to debugging the quest system, I implemented a start screen UI and an in-game welcome interface that provides hints about what the player should do, to improve the player’s onboarding experience. Previously, the game started immediately when the scene loaded, which could be confusing for first-time players.


To address this, I created a start screen using Unity’s **Canvas UI system**, including a **Start Game** button that allows players to begin the game intentionally. The UI appears when the scene first loads and remains visible until the player presses the start button.


I also added an in-game welcome and hint panel that briefly introduces the town and informs players about basic controls and objectives. This interface provides early guidance before players begin interacting with NPCs and quests.


These UI additions improve the player’s first interaction with the game and make the transition from the start screen to gameplay more structured and understandable.


### Tina Meng
Contribution:

1. The old dialogue system puts dialogue content in clerk's code and uses ui script to show the dialogue directly. I found that this cannot meet professor's requirement of scriptable object so I created scriptable objects and an entirely new dialogue system. I created, wrote and debugged codes for the new dialogue system and fixed code for the item appear. 
    - Dialogue Node
        - All codes
    - DialogueManager
        - StartDialogue (create)
        - ShowCurrentNode (create)
        - BuildOptionsFromNode (create)
        - FinishAndClose (rewrite)
        - AdvanceNodeOrFinish (create)
        - OnChoiceSelected (rewrite)
        - ShowNodeChoice (create)
    - Clerk
        - Interact (rewrite)
        - OnQuestTrainingSelected (rewrite)
    - DialogueView
        - EnsureView (changed details)
    - Cat
        - CanInteract (rewrite)
2. I built all scriptable objects for the game and improved dialogue content for the following nodes
    - Training Not Started
    - Training Play Assigned
    - Training Completed
    - Menu
3. I wrote codes and built the ending UI for the game to inform players that they have finished the game and they are free to leave
    - EndGameNotice
        - Create script
        - ShowNotice
        - ContinueGame
        - RestartGame
            - I deleted this function because I found that it couldn't work perfectly in the game
    - Quest
        - Update
        - CheckGameEnd
    - GameController
        - AreAllQuestsCompleted


As I mentioned in pre-learning quiz of week 10, I found that scaling of the game project is extremely important because when I was building the new dialogue system, I have to figure out what parts of the code are still useful and what are not. 
### Yan Zhang


## Open-Source Assets
- [Guide on how to make dialogue](https://www.youtube.com/watch?v=_nRzoTzeyxU) -Learning how to structure and trigger the dialogue system in Unity
- [Guide on how to import skybox](https://www.youtube.com/shorts/oDXfDGw-rwg) -Learning how to correctly import and apply a skybox to the scene
- [Skybox](https://assetstore.unity.com/packages/2d/textures-materials/sky/skybox-series-free-103633) -Providing the background sky environment for the town scene
- [3D model for environment "Fantasy Landscape"](https://assetstore.unity.com/packages/3d/environments/fantasy-landscape-103573) -Creating environmental elements such as terrain, trees, rocks, and decorative objects
- [Cat model](https://assetstore.unity.com/packages/3d/characters/animals/mammals/stylized-cats-pack-324086) -The interactive cat NPC character
- [Cat item](https://assetstore.unity.com/packages/3d/props/interior/cat-s-paradise-constructor-329708) -Props related to the cat quest interaction
- [NPC 3D model](https://assetstore.unity.com/packages/3d/characters/humanoids/casual-1-anime-girl-characters-185076) -The interactive clerk NPC character
- [Cat eating sound effect](https://pixabay.com/sound-effects/search/cat%20eat/) -Audio feedback during the cat eating animation
- [Background music](https://assetstore.unity.com/packages/audio/music/music-cat-in-a-box-free-single-306461) -Providing background music during gameplay
- [Start Game UI font](https://www.dafont.com/search.php?q=magicretro) -Provide artistic font for the Start Game UI
