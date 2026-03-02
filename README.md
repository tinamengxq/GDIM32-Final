# GDIM32-Final
## Check-In
### Devlog Questions

#### Team Devlog:
1. Prompt A:

Describe how you used one of the tools that we learned in Week 6- Gizmos, the Unity profiler, breakpoints, or version control techniques- to solve a technical issue with your project. Make sure to clearly describe what the problem was; why you picked the tool that you chose; and how you diagnosed and solved the issue.
2. Prompt B:

Describe how intermediate-level vector math is being used in your project. This could be any of following the vector topics we talked about in Weeks 7-8: dot products, surface normals, coordinate space transformations, or raycasting (or sphere-casting, box-casting, etc). Make sure to clearly describe the feature that required this math; what concept you used and why it's relevant; and how the code works.

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
1. I implemented and refined the first-person mouse look system in the Plyaer script.
   This included:

   - Enabling full 360° horizontal rotation
   - Clamping vertical rotation between -90 and 90 degrees to prevent unnatural flipping
   - Adjusting the `lookSensitivity` variable to improve responsiveness
   - Tuning sensitivity values so the player does not need excessive mouse movement to reach a desired viewing angle

Through testing and iteration, I modified sensitivity values to achieve smoother and more natural camera control. At the beginning, the rotation felt too slow and required large mouse movement. These adjustments directly improved the gameplay experience.


2. I also worked on refining how interactable objects are detected and how interaction is triggered.
   The system evaluates:

   - View direction using Vector3.Dot
   - Distance between the player and target
   - A threshold value to determine whether an object is within interaction range
  
I adjusted parameters such as:

- viewDotThreshold
- Maximum interaction distance
- Detection conditions to ensure that pressing the F key reliably triggers interaction

Sometimes objects were not detected even when the player was looking at them. I tested different angle and distance values and adjusted them to make the interaction feel more natural and consistent. My goal was to ensure that the full interaction flow works smoothly: detection, prompt display, and successful triggering.<br><br>



**Additional contributions:**

In addition to gameplay systems, I slightly adjusted the Directional Light’s position and color to better differentiate it from the Spot Light.
I also collaborated with teammates in searching for environmental assets and contributed to a small portion of scene setup.

Besides coding, I often played the game during development to test interaction feel and identify issues. When I noticed detection problems or camera control discomfort, I reported them and adjusted the related values. Although everyone participated in playtesting, I personally focused more on identifying small usability issues, questioning them and together we found solutions out to fix them.

These contributions were supportive in nature, while my primary focus remained on player control and interaction functionality.<br><br>



**Reflection:**

Our original proposal outlined the general interaction concept quite clearly, but during development, I realized that implementing interaction systems requires careful tuning of numerical thresholds and detection logic. I realized that even a small change in the dot threshold (for example from 0.7 to 0.85) significantly changed how strict the detection felt.

Small values such as camera sensitivity, interaction distance, and view-angle thresholds significantly impact player experience.

In the future project, I would plan player control tuning earlier in development if needed. And define technical interaction methods more clearly in the proposal.
I also learned that documenting value adjustments during testing is important for better tracking and team communication.


Overall, working on the player control and interaction system helped me understand how core gameplay feel depends heavily on precise implementation and iteration.



### Yan Zhang 
I mainly continue to add things on the basis of what others have built before. I first imported the cat's resource package, put the cat prefab into the scene, assigned it an Animator Controller, and then wrote code in Cat.cs to control the animation. Now I use Animator.Play(stateName) to switch different states. The cat itself is also an interactive object. It still uses the original NPC interaction logic, triggered by Interact(), so the whole system is connected to the original interaction structure.


Then I finished the logic of the cat-related item. The quest system is managed by GameController.cs. The quest state is controlled there. When the state changes, an event is triggered. UI.cs refreshes the quest panel when it receives the event. In this part, I mainly rationalized the logic, because although the variables were changing at the beginning, the UI was not updated, and it looked like there was no response. And I have completely connected the chain of "quest state change → trigger event → UI update".


On the side of the dialogue system, the previous team members probably set up a basic structure, but the actual logic was not finished. Instead of directly following her structure, I rearranged the process of triggering and advancing the dialogue, and made Interact() call the DialogueManager. After the conversation ends, it can also affect the quest state, so the dialogue system and the quest system are connected.


In addition, I also added global background music and got a resident AudioManager (Singleton) to control the background sound.


####Reflection：


I think our plan is quite detailed. It is really useful when you first start writing code. Just follow it step by step. But in the middle of the period, I found the problem - the proposal was too idealistic and didn't think much about whether I could do it or not. Some designs look very good, but when you really write them, you will find that they are not so simple. In the end, many places can only be simplified or achieved in other ways.


When I do the project in the future, I will be more realistic at the proposal stage. First, I will find out how much I can do, which functions must be completed, and which can be added later.


## Final Submission
### Group Devlog
Put your group Devlog here.


## Open-Source Assets
- [Guide on how to make dialogue](https://www.youtube.com/watch?v=_nRzoTzeyxU)
- [Guide on how to import skybox](https://www.youtube.com/shorts/oDXfDGw-rwg)
- [Skybox](https://assetstore.unity.com/packages/2d/textures-materials/sky/skybox-series-free-103633)
- [3D model for environment "Fantasy Landscape"](https://assetstore.unity.com/packages/3d/environments/fantasy-landscape-103573)
- [Cat mode](https://assetstore.unity.com/packages/3d/characters/animals/mammals/stylized-cats-pack-324086)
- [Cat item](https://assetstore.unity.com/packages/3d/props/interior/cat-s-paradise-constructor-329708)
- [NPC 3D modle](https://assetstore.unity.com/packages/3d/characters/humanoids/casual-1-anime-girl-characters-185076)
- [Bckground music](https://assetstore.unity.com/packages/audio/music/music-cat-in-a-box-free-single-306461)
