# Code
## PlayerMovement.cs
This script handles all player movement, mechanics and physics (i.e. Simple Movement, Character Flipping, Jumping, Chouching, Gravity Effects, Animation).

## CameraFollow.cs
This script handles the main camera. It follows the player smoothly and has the ability to zoom in and out to certain limits.

## Weapon.cs
This script handles the rocket firing. It shoots rays, apply damage and collects the information of the object being shot. It is still not completed. It is supposed to have a 360-degree aiming function along the y-axis (that is partially implemented).

## Target.cs
This script gives a health variable to the target object. It has a function that deducts the damage from the health variable, and destroys the target if the health reaches 0 (or less).