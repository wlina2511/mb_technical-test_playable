# MadBox project : Unity


###  Time spent on this part

I have spent a total of around **7** hours on Unity, split as following :
- Around **1** setting out the background.
- Around **1** coding the player and camera movements.
- Around **3** implementing the obstacles, the timer and the slider. 
- The last hour was me messing around and trying things that did not make it to the final game.

###  Architectural and technical choices
Iused Unity 2019.1.5f1 as requested, and chose to have my architecture as follows : 
![alt text](https://i.stack.imgur.com/m7RqR.png "Image1")

- The **GameController** GameObject is an empty one with the **GameController** script attached to it.
- The **Path** GameObject is where the LineRenderer is set, and will be the points that the player will follow.
- The **Player**, **Background** and **Obstacles** GameObject speak for themselves.
- And lastly, the **UI** GameObject contains the three **Canvases** we are using to use the application :
    - **MainMenu** is the Canvas where the Leaderboard is displayed. This screen contains only one button, used to launch the game.
    - **GameCanvas** is the Canvas shown In Game, where the timer and the slider are displayed.
    - **EndCanvas** is the Canvas displayed at the end, with the replay button and the player's score.

### What was hard
I had to go back to my highschool notes to do a pendulum but otherwise the rest of the development went smooth.
The biggest setback was the camera movements, and in certain edge cases (which I can't seem to understand), the camera will have unplanned behaviours.    

### What I would do to push the project a step forward
Some design might go a long way. Player model and the menu design are greatly diminished in this demo. :(

### Project startup

##### Unity Version

The version I used for this project was as I said in the intro the 2019.1.5f1 .

##### Step by step guide

Open **Unity** and the project folder. Click **Play** and try to do the best possible score ! 
To play, you only need to hold left click and your character will move, if you let it go the character will stop.
Have fun !

### Final thoughts and bibliography

I would like to thanks everyone at MadBox who has been so nice to me and gave me a shot at joining this wonderful company.
That was a great experience !

   
   

   
