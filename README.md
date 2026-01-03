# Minimum Wage Inferno - Unity Build

## DL1 (12/12/25) Tilemaps
When importing a tilemap to Unity, you'll need to create a `grid` and `2D_Tilemap`in the **Hierarchy**. In the **Inspector**, change the `Cell Size` to whatever pixel resolution you're working at (eg. 0.16, 0.64, 1.28). Create an "Art" folder for your visual assets, and another within it called "Tiles"; this will be referenced later. Drag your tilesheet into the Art folder and return your attention to the **Inspector**. Change `Sprite Mode` to 'Multiple', the `Pixels Per Unit`to your desired resolution---also (if using pixel art) change `Filter Mode` to 'Point (no filter)' and `Compression` to 'None'.

This is where I zoned out and missed a step the first time. Go to **Window** -> **2D** -> **Sprite Editor**. Slice your selected tilemap accordingly. ***THEN*** you can go into your **Tile Palette** window and create a New Palette. Name it, set `Grid` to 'Rectange' and `Size` to 'Manual', then input your pixel resolution below. It'll look something like this:

<img width="519" height="332" alt="image" src="https://github.com/user-attachments/assets/b42ba9b5-8d1a-477e-96b7-7e8a698f86b5" />

From here, you can start painting on the `Tilemap` layer in your **Hierarchy**. It's worth having at least two; one for collision objects and one for non-collision. In the **Inspector** you'll need to add a `Tilemap Collider 2D` to the Collision Tilemap.

> [!IMPORTANT]
> Under `Tilemap Renderer` you'll find "Order in Layer". Use this to arrange your layers, the higher number shows up over the other tiles, which is needed for overlapping sprites and stuff.

## DL2 (13/12/25) Gridded Movement + Point & Click Controls
I followed a convoluted tutorial that had me typing over 300 lines across 5 different scripts, none of which I understand too well. What I **do** understand is that AStarGrid (as used in Godot) is not present in Unity, and has to be programmed from scratch. AStarGrid is a pathfinding algorithm, which detects obstacles and tries to find the faster path from the `startingPosition` to the `targetPosition`. I'm unsure if it's down to how my tilemaps are set up or my lack of understanding of how prefabs work, but currently my character moves across all tiles regardless of collision. Because my character is taller than the 128 grid boxes, I think the code doesn't fully grasp which part of him is meant to be standing in the cell.

I'll see if I can retry this with a simpler tutorial. I despise that I roughly understand what's going on, but not how it all works, because it means I can't reverse engineer it to my own specifications just yet.

## DL3 (14/12/25) Cont.
> [!NOTE]]
> In order to make a prefab, you have to drag the object from your **Hierarchy** into your Assets folder (label an empty folder as 'prefabs' in advance. This means you can drag and drop the prefab into any scene you need, with all its attributes in tact, like code or settings)

In this instance, I created an empty 2D object named 'Node', gave it a shape using the `Inspector`, and attached a Node Script to it,

```
public class Node : MonoBehaviour
{
    public Node cameFrom;
    public List<Node> connections;

    public float gScore;
    public float hScore;

    public float FScore()
    {
        return gScore + hScore;
    }
}
```

I've noticed there are different ways to make gridded movement, so far I've tried one using Tilemaps and Colliders, and another using Nodes and Lists of Connections. For what it's worth, the former may not need colliders since there's no health or projectiles to warrant a hit-box- actually, it *may* be necessary if I'm involving Demon Hunters/Exorcists as enemies.

Either way, today has proven unsuccessful in getting movement to work. Given how much easier it was in Godot, I have to question the switch... it is frustrating to not be able to figure it out myself. All the tutorials for Unity do not use it the same way I want to, and attempting to splice tutorials never works out.

## DL4 (18/12/25) Post-Appointment Restructure
(Technically done yesterday) I had an appointment with David to figure out how to get the movement working. He suggested moving away from the gridded movement for now and opting to use NavMesh for AI pathfinding. So far it's working fine on both Unity 6 and Unity 2022 (though I have issues when importing them over).

There are a few things I'd need to iron out:
1. The employees with block and collide into each other - need to allow them to pass through each other.
2. The acceleration and pathfinding is a little floaty for my liking; they take wide turns around corners.
3. When clicking multiple areas, the idle employee will change course and go to the most recent target.
4. Currently, clicking anywhere on the map will not get the employees to move; only when ordered to interact with something
5. We were in the middle of figuring out how to set up the Store Manager so that the employees have a hierarchy of who can go to which tasks, updating their status as they move

## DL5 (19/12/25) Cont.
- To disable collision between NavMesh Agents, set `Obstacle Avoidance Quality` to `None`
- Currently the employees are moving towards the clicked object rather than the navPoint. This has something to do with the StoreManager script, but my attempts to add the navPoint only result in nobody moving, or only one moving despite all of them being Idle.

<img width="368" height="190" alt="image" src="https://github.com/user-attachments/assets/4394dab7-2dd6-4be8-ac29-334d786760cc" />

<img width="529" height="596" alt="image" src="https://github.com/user-attachments/assets/a3f86b0c-8bfc-40b9-b210-63bb31c3fa83" />

- Need to find a method of switching the employees status as and when they're moving.
- Still need to create a hierarchy that allows one employee to move at a time.

For now, since I won't be seeing anyone for a few weeks, it may be best to either focus on the art direction for the time being, or try to program the menus and maybe a VN Cutscene. We'll see.

## DL6 (03/01/26) Timer & Customer Spawn Rate
I've managed to implement a visual timer where every second equates to 5 in-game minutes, and once it reaches 6pm it changes to say 'Closed'.
I've also implemented a random spawn rate for customers (will need to play with the randomised range to see what works for an easier or harder difficulty), though this may need to be adjusted when I have a list of different types of customers (single, couple or groups, for instance). I'm not sure how to change where they spawn (or if it matters) and how to program their movement between set locations, as their first instinct will be to occupy the nearest available chair to the door, and after being served at a table will make their way to the till, before leaving through the door. This only means they'll have 3 points to walk towards when they are moving. Tomorrow we'll try to implement the VN code, since I'd rather consult David on how to proceed with fixing my employee code, and how to go about the customer code.
