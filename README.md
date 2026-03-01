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

### Team Member Name 2
Put your individual check-in Devlog here.
### Team Member Name 3
Put your individual check-in Devlog here.


## Final Submission
### Group Devlog
Put your group Devlog here.


### Team Member Name 1
Put your individual final Devlog here.
### Team Member Name 2
Put your individual final Devlog here.
### Team Member Name 3
Put your individual final Devlog here.

## Open-Source Assets
- [Guide on how to make dialogue](https://www.youtube.com/watch?v=_nRzoTzeyxU)
- [Guide on how to import skybox](https://www.youtube.com/shorts/oDXfDGw-rwg)
- [Skybox](https://assetstore.unity.com/packages/2d/textures-materials/sky/skybox-series-free-103633)
- [3D model for environment "Fantasy Landscape"](https://assetstore.unity.com/packages/3d/environments/fantasy-landscape-103573)