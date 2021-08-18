# DragonGame
# Written by Eran Meir

This game is currently under development and I code this in Unity & Visual Studio during my free time.
Its only purpose is just for fun.

Here I have added only some of what I wrote and added to my game, I did not include prefarbs and scripts of other people, as it's not my right to do so.

## Player
### Player Level System
This system is implemented with using MVC design pattern
- ```LevelSystemModel.cs``` the model of the player level system
- ```LevelSliderView.cs``` the view component that presents the UI
- ```LevelSystemController.cs``` the player level system controller, controls the level slider view and the level system model

## Enemy
### ```Enemy.cs```
This class is an abstract class that contains the basics of every Enemy.
- #### ```Skeleton.cs```
  - This class inherits from Enemy and implements the Skeleton behaviour
  - It also has an **extremely basic** level system, which according to that things like stats will be set for the skeleton (Stronger Player -> Stronger Enemies)
  - It also has a health system that can interact with the player if the skeleton was hit.

### Enemy Health System
This system is implemented with using MVC design pattern
- ```EnemyHealthSystemModel.cs``` the model of the enemy health system
- ```EnemyHealthSystemView.cs``` the view component that presents the UI above the enemy
- ```EnemyHealthSystemController.cs``` the enemy health system controller, controls the enemy health system view and the enemy health system model
