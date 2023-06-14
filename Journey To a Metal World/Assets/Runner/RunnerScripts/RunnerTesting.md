# Runner Game Testing Notes

Since there are so many moving parts inside this game I'm taking out the time to write out a list of things to test and how to do so. After changes to anything relating through the runner, running through these will probably be a good way to make sure functionality has not been lost

### Before Game Starts 

- Player starts with 3 lives. There should be 3 orbiter sprites at the top middle part of the screen
- The music is working 
    - Note: If you were in the Unity editor and opened the **Runner** scene directly, audio will not work. This is because audio is overall controlled by an object that is made in the **Title** Scene 
    - Additionally, when testing, remember if you used the mute button or not. And the icon should change based on if mute is active or not 
- Combo is 0
- Score (top right) is 0
- Progress bar at the bottom has the mini orbital at the Start side (left side of screen)
- Instructions page is in the center of the screen
    - If it is not visible, this is a big problem. Check if it was deleted (if so it would need to be remade) although I'd guess the more likely problem would be if it was made inactive to check something behind it in the editor and then never made active again 
- The movement controls shouldn't work until after you click the ok button 
- The background isn't moving and meteoroids will not spawn until after the ok button is clicked 
- The Restart, Main Menu, and Mute buttons shouldn't be usable until after the OK button is pressed
    - If they can be used before the OK button is pressed, that alone is not necessarily a game breaking thing but it may be a sign that some layers got incorrectly
- The ok button lets the game begin


### Game Has started
- The background starts moving after the ok button is pressed
- Meteoroids start being spawned in after the ok is pressed (with short dealys after each meteoroid)
    - If meteoroids are spawning literally on top of each other then there is a problem. 
    - If literally every single meteoroid is spawning in the same lane there is a problem
- The points increase after moving past each meteoroid
- There should be a combo sound effect and the amount of points gained should increase per meteoroid passed for each combo  multiple eleven reached
- The meteoroids and background should gradually speed up (although there is a top speed that it will not get faster than)
- The orbiter/player itself should have a constant speed for the entire game


### After hitting a meteoroid 
- Check that all meteoroids change to a slower speed (most noticeable if you were going really fast)
    - Exception: if you were already going really slow it might not be noticeablely slower
    - Exception: if you somehow managed to continuously hit the meteoroid then you probably wouldn't have gained enough speed to notice a cooldown however being able to lose multiple lives from the same meteoroid is also something that shouldn't happen
- You should have one less life icon at the top middle of the screen
    - If you lost more than 1 life fromt the samse meteoroid that is a sign that something went wrong. 
- The combo should be reset to 0
- If you have 0 lives you should get the game over screen
- The rotation of the player/orbiter should not change 
    - If it does it is a more minor bug as the game is more playable but the hit box of the player will be different than it was when the player was straight 
- Note, the orbital should turn red and you should be granted temporary invinciblity after hitting a meteoroid

### Game Reaches Ending
- The mini orbital at the progress bar should be at the Finish side 
- The game should end (with a score screen) soon after Psyche spawns
    - if Psyche goes from entering the screen to off the camera view (going off the left side of the screen) then there is a problem
- Alternately if you lost all your lives before then you should still get a score screen
- If you got a new high score your score and high score should equal each other
- From the game over screen the red buttons for restart and main menu should work but the ones at the top left (restart, main menu, and mute) should not
    - If the top left buttons still function it is not a major problem unless they crash the game

### Misc / More obscure bugs
- Directly try to run into a meteoroid, if you can push the meteoroid around consistently this is a bad sign and may lead to the potential for using it as a shield against other meteoroids
    - Currently I think the speed of the game makes this impossible but if you make further changes look out for it. 
- If you skim the edges of a meteoroid, check if you lost more than one life. You should only lose one 
    - If you lose more than one you will need to check the methods that do damage calculations and whether or not the algorithm that stops the same meteoroid from damaging you more than once is doing its job
- If the game is stuttering (particularly the meteoroids) , check the frame rates of the computer it is running on. I believe this stems from if you get a low enough frame rate, you can perieve the individual movements. This may also impact how closely the meteoroids are spawning to each other. I have attempted to make the movement frame rate independent but it feels like this can sometimes still come up
- The hit box on the buttons are as close as I can get it but if you do click the very very edges of the buttons, I have a feeling it is possible to miss the hit box. That said, if you click anywhere near the middle of the buttons you should be completely fine. 