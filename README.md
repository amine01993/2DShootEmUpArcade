# 2DShootEmUpArcade

By Amine Errafii (101651263)

===================================================
======= REFERENCES
===================================================

CatoDevs. 2021. Unity Tutorial : How to Import a Custom Font into Unity
https://www.youtube.com/watch?v=gMd0xDEFE20

Simplest way to target GameObjects properties when it triggers/collides?
https://stackoverflow.com/questions/43934277/simplest-way-to-target-gameobjects-properties-when-it-triggers-collides
(I used this to learn how to get Gameobject of collision targets after a collision occurs)

how to get relative position of a game object with respect to parent
https://discussions.unity.com/t/how-to-get-relative-position-of-a-game-object-with-respect-to-parent/131688
(I used this to position enemy bullets relative to the ship)

How to get object instance thru tag ?
https://discussions.unity.com/t/how-to-get-object-instance-thru-tag/813463
(This helped me find objects/instances using their tag)

How do I make an object look at an another object in Top-Down 2D? ( C# )
https://discussions.unity.com/t/how-do-i-make-an-object-look-at-an-another-object-in-top-down-2d-c/104849
(I used this to make an enemy ship look towards the player)

trying to Lerp a rotation…
https://discussions.unity.com/t/trying-to-lerp-a-rotation/102606/2
(This helped animate enemy rotation)

OnTriggerStay2D stops working randomly.
https://discussions.unity.com/t/ontriggerstay2d-stops-working-randomly/139466
(OnTriggerStay2D stops being called after a while because it is on mode lazy per default)

official doc for the splines package (I used it to animate gameobjects alongside a predefined path)
https://docs.unity3d.com/Packages/com.unity.splines@2.8/api/index.html

Add and remove components by scripting
https://discussions.unity.com/t/add-and-remove-components-by-scripting/400390/3
(Destroy can be used on gameobjects as well as their components)

Detect if object is visible on camera
https://discussions.unity.com/t/detect-if-object-is-visible-on-camera/851643
(Help me check if a gameobject is visible on camera)

## Milstone 4

Rotate a Vector3 direction
https://discussions.unity.com/t/rotate-a-vector3-direction/14722
(Provided the line of code for rotating a vector direction by angle along an axis)

OnTriggerExit() is not called when a object inside is destroyed
https://www.reddit.com/r/Unity3D/comments/y2n26d/ontriggerexit_is_not_called_when_a_object_inside/
I placed a rectangle on the screen with similar dimensions to keep track of objects on screen, objects destroyed on screen were still being marked as tracked, so after I checked that, I used OnDestroy to untrack these objects.

UNT0024 Give priority to scalar calculations over vector calculations
https://github.com/microsoft/Microsoft.Unity.Analyzers/blob/main/doc/UNT0024.md
Showed me that in a multiplication of more than 2 factors, starting with scalars from the left is faster

Events & Delegates in Unity
https://gamedevbeginner.com/events-and-delegates-in-unity/
I used this article as guide to help me communicate between separate monobehaviour game objects

[Solved] Where is force applied on Rigidbodies?
https://discussions.unity.com/t/solved-where-is-force-applied-on-rigidbodies/731510
Give reference to the right documentation that led me to the correct method for applying force to my collecible game object

bouncing an object of a wall.
https://discussions.unity.com/t/bouncing-an-object-of-a-wall/907446/10
I wished to make my collectible bounce of the edges of the screen, this thread was of great help.

Attribute (role-playing games)
https://en.wikipedia.org/wiki/Attribute_(role-playing_games)
I leaned that player statistic, (primary) characteristic or ability are called attributes as a general term

changing TextMeshPro Text (from UI) via script
https://discussions.unity.com/t/changing-textmeshpro-text-from-ui-via-script/659074/5
I used textmeshpro component for a few canvas elements. This discussion shows the correct class name, which allowed to use the component on a script

https://discussions.unity.com/t/best-way-to-initiate-waves-of-enemies-2d-shooter/816637/3
This discussion inspired me to mix up wave with enemy formation (constant) with other waves of randomly instantianted enemies

Is there a way to create a table in the inspector?
https://discussions.unity.com/t/is-there-a-way-to-create-a-table-in-the-inspector/732861
This led me to learn about UI Toolkit and create a custom UI in the inspector

Cannot finish the game in Unity using Application.Quit() [duplicate]
https://stackoverflow.com/questions/70437401/cannot-finish-the-game-in-unity-using-application-quit
Provided script for quitting the game in build and editor

## Milestone 5

jsfxr - 8 bit sound maker and online sfx generator
https://sfxr.me/
Used it to generate sound effects

destroy object and play sound?
https://discussions.unity.com/t/destroy-object-and-play-sound/168894/4
Helpt me come up with the idea of creating prefabs for each of the sound effects

Sound is played with a delay.
https://discussions.unity.com/t/sound-is-played-with-a-delay/708928/15
I was using my bluetooth earphones and I was experiencing a delay for each soundfx, at first, I thought it was a unity problem, but this article help me realize that the delay was caused by the bluetooth

Fix Unity Particle System Delay | Not Starting Immediately (2D Sprite Particles)
https://www.youtube.com/watch?v=uBIq0vfYfqU
another delay issue with the particle system that I found how to fix in this video

Scripting With The Animator Component in Unity
https://www.youtube.com/watch?v=I_ilRHeHvIs
This is a good video that introduces the animator component in unity

Make On Hit Flash Effect In Unity 6
https://www.youtube.com/watch?v=oJEq4sPxHhU
Shows how to make hit flash in unity

Animate the scaling of a gameObject
https://discussions.unity.com/t/animate-the-scaling-of-a-gameobject/70235/5
Used this article to animate the health bars

## Assignment 1 GAME1017

Fixing: Scene couldn't be loaded because it has not 
https://www.youtube.com/shorts/ge3koyyH3nc
how to use other scenes

===================================================
======= LICENSES
===================================================

CC BY 4.0 Deed
Author: JanaChumi
https://opengameart.org/content/space-shooter-assets-3

CC0 1.0 Deed
Author: Kenney
https://opengameart.org/content/space-shooter-art

Tilt Neon Designed by Andy Clymer, licensed under the SIL Open Font License 1.1 [Font].
https://fonts.google.com/specimen/Tilt+Neon

CC BY 4.0 Deed
Author: Unnamed
https://opengameart.org/content/17-planet-sprites

CC-BY 3.0
Author: Oblidivm
https://opengameart.org/content/space-shooter-music

CC0
Author: diamonddmgirl
https://opengameart.org/content/32x32-explosion