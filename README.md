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
