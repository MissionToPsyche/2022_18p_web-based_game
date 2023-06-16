using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/**
    The script attached to the orbiter and controls almost anything that is 
    orbiter (player) related
    As a sidenote you will notice it is called an "orbital" in the code. This is
    an unfortunate mix up as we initially thought it was an orbital but the 
    thing being launched is in fact an orbiter. Changing every single instance
    of the word "orbital" being used would be a waste of time and likely result in 
    a lot of bugs though and since this code will not be publicly available 
    I have made the decision to leave it  
*/
public class RunnerOrbital : MonoBehaviour
{
    
    [SerializeField] int MAX_LIVES = 3;
    [SerializeField] float show_speed;
    int lives;
    // float power_bank = 100f;
    float movement_speed = 7f;
    // note to future dev: if you change the movement speed know that you must make sure 
    // that you test it against the meteoroids. Also, having the movement_speed of the orbital
    // beign faster than that of the meteoroids (top speed) seems like a bad idea and 
    // may give the ability to hit the same meteoroids multiple times (with delays between the
    // hits)

    int INVINCIBILITY_FRAME_LENGTH = 72;
    int invinciblity_frames_left = 0;
    int current_collisions = 0; // because we can theoretically hit multiple things at once
    // although with the way the game is currently made that is extremely unlikely
    
    bool damage_effect_on = false;
    // the damage effect is tinting the orbiter red 

    bool movement_allowed= false; // first we start with not being able to move
    List<GameObject> collision_list = new List<GameObject>();

    Vector2 raw_input;
    SpriteRenderer sprite;
    Color orbital_color;
    RunnerScore score_keeper;

    bool game_over = false;
    bool game_won = false;
    bool finished = false; // used to be able to do 
    // the game over or game one process once and not loop

    int COLLISION_MINUS_POINT = -1;
    // I don't want this value to be too big but also don't want it to be zero
    // so it's -1. ... if you get a large enough combo this is miniscule in comparison
    // but losing the combo would be the bigger loss 
    // In the end though, this value can be changed pretty freely BUT make sure to double
    // check that the score isn't going negative still

    RunnerMeteoroidMove stage; 
    RunnerEnemySpawner enemy_spawner;
    RunnerAudioPlayer audio_player;

    [SerializeField] bool apply_camera_shake = true;
    RunnerCameraShake camera_shake; 

    Vector2 min_bounds;
    Vector2 max_bounds;
    [SerializeField] float padding_left = 1f;
    [SerializeField] float padding_right = 1.5f;
    [SerializeField] float padding_top = 1.9f;
    [SerializeField] float padding_bottom = 0.7f;
    RunnerLivesFrontEndManager visual_manager;
    [SerializeField] RunnerGameOverScreen game_over_screen;
    RunnerEnvironmentMovementManager environment_manager;

    /**
        Get the camera immediately 
    */
    void Awake() 
    {
        this.camera_shake = Camera.main.GetComponent<RunnerCameraShake>(); 
    }

    /**
        Things we need tos tart with so all the other parts work
    */
    void Start()
    {
        this.sprite = GetComponent<SpriteRenderer>(); // get the sprite
        this.orbital_color = sprite.color; // get the current color so we can return to it later
        this.score_keeper = FindObjectOfType<RunnerScore>(); // the points functions will be 
        // needed since we get points after the orbital moves past the meteoroids 
        // and we know when that happens here


        this.visual_manager = FindObjectOfType<RunnerLivesFrontEndManager>();
        // needed for spwaning of life sprites 

        this.enemy_spawner = FindObjectOfType<RunnerEnemySpawner>(); // we need to be able to tell the 
        //meteoroids to stop spawning on loss so a referene to the enemySpawner

        this.environment_manager = FindObjectOfType<RunnerEnvironmentMovementManager>();
        // needed fr in general manipulation of environment 

        this.lives = MAX_LIVES; // setting the lives to the preset constant

        audio_player = FindObjectOfType<RunnerAudioPlayer>(); 
        // need the audio player to run the collision sounds
        
        InitBounds(); // need to enforce bounds on the game so that the player
        // cannot move off screen

    }

    // Update is called once per frame
    void Update()
    {
        
        this.game_won = environment_manager.GetIsWin();  // check if we won

        // moving the orbital
        OrbitalMovement();

        // do damage calculations 
        DamageApplied(this.current_collisions);

        // deduct invincibility frames if there are any left
        if (this.invinciblity_frames_left > 0)
        {
            this.invinciblity_frames_left--; 
        }

        //game_over and game_won are what they sound like
        // finished is used so that we only do this conditional once and don't 
        // continuously bring up the score screen every signle frame after the game
        // is over
        if (this.finished == false && (this.game_over == true || this.game_won == true) )
        {
            
            if (this.game_won == false)
            {
                // well, this gameLostProcess() == true is a leftover and it should always
                // be true but I will leave it like this so that it's easier for a future dev
                // to make a customized loss process
                if (gameLostProcess() == true)
                {
                    BringUpScoreScreen();
                } 
            }
            else
            {
                BringUpScoreScreen();
            }
            this.finished = true; // this is what makes this entire if statement 
            // only execute once
            
        }

    }

    /**
    Initializes the bounds of the game
    */
    void InitBounds()
    {
        Camera main_camera = Camera.main;
        min_bounds = main_camera.ViewportToWorldPoint(new Vector2(0, 0));
        max_bounds = main_camera.ViewportToWorldPoint(new Vector2(1, 1));
    }

    /**
        Public method to turn on the orbiter's ability to move
    */
    public void AllowOrbitalMovement()
    {
        this.movement_allowed = true;
    }

    /*
        Takes care of calculating orbital movement whiel also keeping it within bounds
    */
    void OrbitalMovement()
    {
        // if movement was turned off er exit early
        if (this.movement_allowed == false)
        {
            return;
        }

        Vector2 delta = this.raw_input * movement_speed * Time.deltaTime;
        // do the calculation for the correct direction


        this.show_speed = movement_speed * Time.deltaTime; // for testing purposes, display the speed 

        Vector2 new_position = new Vector2();
        new_position.x = Mathf.Clamp(transform.position.x + delta.x, this.min_bounds.x + this.padding_left, 
            this.max_bounds.x - this.padding_right);
        new_position.y = Mathf.Clamp(transform.position.y + delta.y, this.min_bounds.y + this.padding_bottom, 
            this.max_bounds.y - this.padding_top);
        // come up with the new position

        transform.position = new_position; // change the position of the orbiter to the new position

        transform.localRotation = Quaternion.Euler(0,0,0); // keep no rotation even after impact
        // because otherwise hitting meteoroids can knock the orbiter into a skewed rotation
    }


    /**
        required for the movement to work. This reads the movement from the keyboard
        and saves it into raw input
    */
    void OnMove(InputValue value)
    {
        this.raw_input = value.Get<Vector2>();
        // Debug.Log("move value is " + this.raw_input);
    }

    /**
        This is a Unity method 
        @other is the trigger that we're entering
        In the Runner game, the point boxes are triggers and I can 
        use Unity's OnTriggerEnter2d as the base of the point system.
        Enter the point box trigger. Run that pointbox's function. gain
        points from it unless we've hit the meteoroid that the point box
        was associated with. Also, each pointbox can only give you points once

        In general the idea is:
         get points for moving past meteoroids [without getting hit]
    */
    void OnTriggerEnter2D(Collider2D other) 
    {
        if (this.game_over == true)
        {
            return;
        }
        // if it's game over we are not able to gain any more points
        // else if the thing that we're hitting the trigger of is tagged as a point box
        // we do the points procedures
        else if (other.tag == "PointBox")
        {
            RunnerMeteoroidPoints pointManager = other.GetComponent<RunnerMeteoroidPoints>();
            pointManager.ReachedPointsArea();   
        }
    }


    /**
        when we collide with something we increase the count of how many things we are currently
        colliding with. This will be used to determine damage later
        Collision2D other is representing the thing we collided with in case we need info from it
        (in this case we might)
    */
    void OnCollisionEnter2D(Collision2D other)
    {
        // if it's somthing we ignore collisions with we don't do anything more
        if (other.gameObject.tag == "CollisionIgnore")
        {
            return;
        }
        this.current_collisions++;
        // when we're entering a collision box we increase the collision count and then add the 
        // object we collided with to the collision list
        this.collision_list.Add(other.gameObject);
        other.gameObject.transform.BroadcastMessage("SetPointTotalToZero");
        // for any object we hit, we need them to run the method SetPointTotalToZero
        // as we should not be able to gain any points from passing it if we hit it

        stage.ResetSpeed();
        // reset the speed of the meteoroids if we've hit one

    }


    /**
        when we stop colliding with something we decrease the count of how many things we are currently
        colliding with. By implementation, this number should never go below 0 
        Collision2D other is representing the thing we collided with in case we need info from it
    */
    void OnCollisionExit2D(Collision2D other) 
    {
        // once more, if it's tagged as CollisionIgnore, we ignore it
        if (other.gameObject.tag == "CollisionIgnore")
        {
            return;
        }

        this.collision_list.Remove(other.gameObject);
        // remove the object we added to the list from when we entered its collision box

        this.current_collisions--; // decrease the [current] collision count by 1    

    }

    /**
    Applies damage (minus 1 life) to the Orbital if it needs to be done and accounts for 
    invinciblity frames and any other special effects related to damage
    Specifically:
        if the orbital is currently colliding with something and has no invincibility frames left
        we can run the sound clip for a collision

    @collision_count paramter = how many things are currently colliding with the orbital
    returns the amount of damage applied (as of right now always 1 or 0)    
    */
    int DamageApplied(int collision_count)
    {
        if (this.lives <= 0) // theoretically lives should never be less than 0 but for
        // redundancy I use <= 
        {
            this.game_over = true;
            this.game_won = false;
            return 0;
        }

        // if we have invincibility frames left
        if (this.invinciblity_frames_left > 0)
        {
            // Debug.Log("has " + this.invinciblity_frames_left + " invinciblity frames left. no lives lost. \n Lives left = " + this.lives);
            if (this.invinciblity_frames_left % ((int)(this.INVINCIBILITY_FRAME_LENGTH/2)) == 0)
            //we conditionally turn on and off the color damage effect of tinting the orbiter red
            {
                if (damage_effect_on == true)
                {
                    this.damage_effect_on = false;
                    sprite.color = Color.red;
                    // Debug.Log("orbital should be red");
                }
                else if (damage_effect_on == false)
                {
                    this.damage_effect_on = true;
                    sprite.color = Color.white; // because this is more the tint than the actual color
                    // Debug.Log("orbital is normal color");

                }
            }
            return 0;
        }
        //so if it's here then there was 0 invincibility frames

        this.damage_effect_on = false; // redundant back up damage effect off
        // and double confirm it's the right color
        sprite.color = orbital_color;

        // if there are collisions right now
        if (collision_count > 0)
        {
            this.damage_effect_on = true;
            //decrease the lives of the orbiter 
            DecreaseLives(CalculateDamage()); // these are both
            // methods from this file 
            // I know that damage dealt will always be 1 so it's return 1
            // but in the event that different damage amounts are possible the
            // returns in this method will need to be changed

            activate_camera_shake();
            // what it sounds like. we do a quick camera shake 
            sprite.color = Color.red;
            // Debug.Log("orbital should be red");

            score_keeper.AddToScore(this.COLLISION_MINUS_POINT);
            // we apply the negative point penalty to the score

            // Debug.Log("No invincibility frames left. -1 life. \n Lives left = " + this.lives);
            this.invinciblity_frames_left = this.INVINCIBILITY_FRAME_LENGTH;
            // gain invinciblity frames 

            // if we dont' have any more lives left then we have a 
            // game over with a loss
            if (this.lives == 0)
            {
                this.game_over = true;
                this.game_won = false;
                // orbital_color.a = 255;
                orbital_color.g = 0;
                orbital_color.b = 0;
                sprite.color = orbital_color;
                

                this.invinciblity_frames_left = 0;
                // reset invincibility frames to 0 for consistency
            }
            this.audio_player.PlayCollisionClip();
            // play collision sound
            return 1; // return damage dealt 1
        }
        return 0; // no things currently colliding with object

    }

    

    /**
        Carries out the process we need to do when thre are 0 lives left
        returns true 
    */
    bool gameLostProcess()
    {
        // this.orbital_color.a--;
        // this.sprite.color = orbital_color;
        orbital_color.g = 0;
        orbital_color.b = 0;
        sprite.color = orbital_color;

        // this would stop movement. but for the more interesting and nicer view, 
        // let's instead just let any existing meteoroids keep going but not spawn any more
        this.enemy_spawner.StopSpawningMeteoroids();

        // technically the return true is pointless now and a leftover from when I wanted a
        // fade out that would require this method to be called multiple times
        // I leave it now because there was another part of the code that depends on it
        return true;
        
    }

    /**
        Checks the score to see if it's a new high score, sets it if it is
        And then brings up the score screen and disables movement
        for the orbiter and the background
    */
    void BringUpScoreScreen()
    {
        int current_score = this.score_keeper.GetScore();
        if (this.score_keeper.IsLargerThanCurrentHighScore(current_score) == true)
        {
            this.score_keeper.SetRunnerHighScore(current_score);
        }
        this.game_over_screen.SetupScreen(current_score, this.score_keeper.GetRunnerHighScore(), this.game_won);
        this.movement_allowed = false;
        // disable movement after all lives lost /  game over screen thrown up
        Debug.Log("game Over screen method finished");


        this.environment_manager.StopBackgroundMovement();
        Debug.Log("background movement turn off function called from orbital");
    }

    /**
        Go through the collision list (note this is a currently colliding with list)
        and check for a meteoroid that hasn't had damage applied yet
        if one exists. Apply its damage and return the amount of damage dealt. Otherwise return 0
    */
    int CalculateDamage()
    {
        for (int i = 0; i < this.collision_list.Count; i++)
        {
            //the problem is, the collision is on the main object but it is the child of the object that has the game script
            // that takes care of points and my added blurb about collisions with the script I need to figure out the damage
            // so threfore I need to take the collision's 1's child (it only has 1 hence index 0)'s component (script) named
            // RunnerMeteoroidPoints
            RunnerMeteoroidPoints list_script = collision_list[i].transform.GetChild(0).gameObject.GetComponent<RunnerMeteoroidPoints>();
            int damage = list_script.GetDamageAmount();
            if (damage > 0) // we do not want to gain lives so no dealing with negative damage
            {
                return damage;
            }

        }
        return 0;

    }

    /**
        Decrease the amoount of lives by the int @amount paramter
        This method does know not to go below 0
        This method also deals with decreasing the amount of visual life
        indicators in the UI to reflect losing lives
    */
    void DecreaseLives(int amount)
    {
        int decrease_by = amount;
        if (this.lives < amount)
        {
            decrease_by = this.lives;
            this.lives = 0;
        }
        else
        {
            this.lives-= amount;
        }
        for (int i = 0; i < decrease_by; i++)
        {
            this.visual_manager.DecreaseLifeSprite();
        }
    }

    /**
        Method to run the camera shake when asked
    */
    void activate_camera_shake()
    {
        if (null != this.camera_shake && true == this.apply_camera_shake)
        {
            this.camera_shake.PlayCameraShake();
        }
    }
    
    /**
        Public getter method to return the maxmimum amount of lives
        the orbiter can have
    */
    public int GetMaxLives()
    {
        return this.MAX_LIVES;
    }

    /**
        public Getter to retun the number of lives the orbiter has right now 
    */
    public int GetLives()
    {
        return this.lives;
    }
}
