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
