using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RunnerOrbital : MonoBehaviour
{
    // so for points. moving past a meteoroid without collisions. if collided then penalty and no point gain.
    // use triggers. ah but needs to have a boolean. gain point 1 time. can lose points multiple times

    //need to bound the movement to a max and min at top and bottom to not just go off screen and win that way. mm
    //or just put colliders along the sides. ... that could work. except then I must specify those ones don't 
    // cause damage

    // start having increasing speed with longer no collisions. probably want to cap it somwhere though
    
    [SerializeField] int MAX_LIVES = 3;
    [SerializeField] float show_speed;
    int lives;
    // float power_bank = 100f;
    float movement_speed = 5f;
    int INVINCIBILITY_FRAME_LENGTH = 72;
    int invinciblity_frames_left = 0;
    int current_collisions = 0; // because we can theoretically hit multiple things at once
    bool damage_effect_on = false;
    bool movement_allowed= false;
    List<GameObject> collision_list = new List<GameObject>();

    Vector2 raw_input;
    SpriteRenderer sprite;
    Color orbital_color;
    RunnerScore score_keeper;
    bool game_over = false;
    bool game_won = false;
    bool finished = false;
    int COLLISION_MINUS_POINT = -1;
    RunnerMeteoroidMove stage; 
    RunnerEnemySpawner enemy_spawner;


    Vector2 min_bounds;
    Vector2 max_bounds;
    [SerializeField] float padding_left = 1f;
    [SerializeField] float padding_right = 1.5f;
    [SerializeField] float padding_top = 1.9f;
    [SerializeField] float padding_bottom = 0.7f;
    RunnerLivesFrontEndManager visual_manager;
    [SerializeField] RunnerGameOverScreen game_over_screen;
    RunnerEnvironmentMovementManager environment_manager;
    // Color flash = ThisSprite.GetComponent<SpriteRenderer>().color;
    // flash.

    void Start()
    {
        this.sprite = GetComponent<SpriteRenderer>();
        this.orbital_color = sprite.color;
        this.score_keeper = FindObjectOfType<RunnerScore>();
        this.stage = FindObjectOfType<RunnerMeteoroidMove>();
        this.visual_manager = FindObjectOfType<RunnerLivesFrontEndManager>();
        this.enemy_spawner = FindObjectOfType<RunnerEnemySpawner>();
        this.environment_manager = FindObjectOfType<RunnerEnvironmentMovementManager>();
        this.lives = MAX_LIVES;
        // this.game_over_screen = FindObjectOfType<RunnerGameOverScreen>();
        InitBounds();

    }

    // Update is called once per frame
    void Update()
    {
        // moving the orbital
        // need to 
        this.game_won = environment_manager.GetIsWin(); 
        OrbitalMovement();
        DamageApplied(this.current_collisions);
        if (this.invinciblity_frames_left > 0)
        {
            this.invinciblity_frames_left--; 
        }

        if (this.finished == false && (this.game_over == true || this.game_won == true) )
        {
            // BringUpScoreScreen();
            if (this.game_won == false)
            {
                if (gameLostProcess() == true)
                {
                    BringUpScoreScreen();
                } 
            }
            else
            {
                BringUpScoreScreen();
            }
            this.finished = true;
            
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

    public void AllowOrbitalMovement()
    {
        this.movement_allowed = true;
    }

    /*
        Takes care of calculating orbital movement whiel also keeping it within bounds
    */
    void OrbitalMovement()
    {
        if (this.movement_allowed == false)
        {
            return;
        }

        Vector2 delta = this.raw_input * movement_speed * Time.deltaTime;;
        this.show_speed = movement_speed * Time.deltaTime;
        Vector2 new_position = new Vector2();
        new_position.x = Mathf.Clamp(transform.position.x + delta.x, this.min_bounds.x + this.padding_left, 
            this.max_bounds.x - this.padding_right);
        new_position.y = Mathf.Clamp(transform.position.y + delta.y, this.min_bounds.y + this.padding_bottom, 
            this.max_bounds.y - this.padding_top);
        transform.position = new_position;
        transform.localRotation = Quaternion.Euler(0,0,0); // keep no rotation even after impact
    }


    // intended to work with movement asset. 
    void OnMove(InputValue value)
    {
        //should be getting the result from the input to w and s keys
        this.raw_input = value.Get<Vector2>();
        // Debug.Log("move value is " + this.raw_input);
    }

    /**
        Currently the only trigger is the finish line. It will mark game over 
    */
    void OnTriggerEnter2D(Collider2D other) 
    {
        if (this.game_over == true)
        {
            return;
        }
        else if (other.tag == "PointBox")
        {
            RunnerMeteoroidPoints pointManager = other.GetComponent<RunnerMeteoroidPoints>();
            pointManager.ReachedPointsArea();   
        }
        // else if (other.tag == "Finish" && this.game_over == false) 
        // // game_over == false is to make sure this triggers only once
        // {
        //     this.game_over = true;
        //     this.game_won = true;
        //     environment_manager.StopBackgroundMovement();
        //     // stage.StopMeteoroidMovement();
        //     // Debug.Log("Meteoroid Movement Stopp");
        // }    

    }

    // void OnTrig

    /**
    when we collide with something we increase the count of how many things we are currently
    colliding with. 
    Collision2D other is representing the thing we collided with in case we need info from it
    */
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "CollisionIgnore")
        {
            return;
        }
        this.current_collisions++;
        this.collision_list.Add(other.gameObject);
        other.gameObject.transform.BroadcastMessage("SetPointTotalToZero");
        // other.gameObject.transform.BroadcastMessage()
        stage.ResetSpeed();

    }

    /**
    Attempt to include only allowing being hit once per object
    */
    // https://answers.unity.com/questions/305614/get-script-variable-from-collider.html
    // try this. set the other to be ... hit count?
    // then need another for damage applied boolean
    // then ... do I need a queue to keep track of these so I can keep checking if necessary
    // just keep the script that can check the damage/points so that that is necessary?
    // queue should work because this is fast and we're just adding and dropping things off of it. 
    // ... also doing 1 function to it. to measure hit count and later on we'll have to manage damamge applied
    // count. 

    // go simpler? once the damamge has been applied once we can just change the damage total to 0 and let it "apply"
    // it again later? 
    // skips needing the if statements 
    // void OnCollisionEnter2D(Collision2D other)
    // {
    //     if (other.gameObject.tag == "CollisionIgnore")
    //     {
    //         return;
    //     }
    //     this.current_collisions++;
    //     other.gameObject.transform.BroadcastMessage("SetPointTotalToZero");
    //     // other.gameObject.transform.BroadcastMessage()
    //     stage.ResetSpeed();

    // }



    /**
    when we stop colliding with something we decrease the count of how many things we are currently
    colliding with. By implementation, this number should never go below 0 
    Collision2D other is representing the thing we collided with in case we need info from it
    */
    void OnCollisionExit2D(Collision2D other) 
    {
        if (other.gameObject.tag == "CollisionIgnore")
        {
            return;
        }
        this.collision_list.Remove(other.gameObject);
        this.current_collisions--;    

    }

    /**
    Applies damage (minus 1 life) to the Orbital if it needs to be done and takes care of invincible frame decreasing
    and any other special effects related to damage
    Specifically:
        if the orbital is currently colliding with something and has no invincibility frames left
    collision_count = how many things are currently colliding with the orbital
    returns the amount of damage applied (as of right now always 1 or 0)    
    */
    int DamageApplied(int collision_count)
    {
        if (this.lives <= 0)
        {
            this.game_over = true;
            this.game_won = false;
            return 0;
        }

        if (this.invinciblity_frames_left > 0)
        {
            // Debug.Log("has " + this.invinciblity_frames_left + " invinciblity frames left. no lives lost. \n Lives left = " + this.lives);
            if (this.invinciblity_frames_left % ((int)(this.INVINCIBILITY_FRAME_LENGTH/2)) == 0)
            {
                // orbital_color.a = 0;
                if (damage_effect_on == true)
                {
                    this.damage_effect_on = false;
                    sprite.color = Color.red;
                    Debug.Log("orbital should be red");
                }
                else if (damage_effect_on == false)
                {
                    this.damage_effect_on = true;
                    sprite.color = Color.white;
                    Debug.Log("orbital is normal color");
                    // because this is more the tint than the actual color

                }
            }
            return 0;
        }
        //so if it's here then there was 0 invincibility frames
        this.damage_effect_on = false;
        
        // orbital_color.a = 255;
        sprite.color = orbital_color;
        if (collision_count > 0)
        {
            this.damage_effect_on = true;

            DecreaseLives(CalculateDamage());
            sprite.color = Color.red;
            Debug.Log("orbital should be red");

            score_keeper.AddToScore(this.COLLISION_MINUS_POINT);

            Debug.Log("No invincibility frames left. -1 life. \n Lives left = " + this.lives);
            this.invinciblity_frames_left = this.INVINCIBILITY_FRAME_LENGTH;
            if (this.lives == 0)
            {
                this.game_over = true;
                this.game_won = false;
                // orbital_color.a = 255;
                orbital_color.g = 0;
                orbital_color.b = 0;
                sprite.color = orbital_color;
                this.invinciblity_frames_left = 0;
            }
            return 1;
        }
        return 0; // no things currently colliding with object

    }

    

    /**
    upon 0 lives left, fade out the player orbital. may integrate restart into this or turn it into separate
    returns true upon game lost process completing (sprite goes fully invisible)
    */
    bool gameLostProcess()
    {
        // this.orbital_color.a--;
        // this.sprite.color = orbital_color;
        orbital_color.g = 0;
        orbital_color.b = 0;
        sprite.color = orbital_color;

        // this.stage.StopMeteoroidMovement();
        // this would stop movement. but for the more interesting and nicer view, 
        // let's instead just let any existing meteoroids keep going but not spawn any more
        this.enemy_spawner.StopSpawningMeteoroids();
        // if (orbital_color.a == 0)
        // {
        //     return true;
        // }
        // return false;
        return true;
    }

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
        // int result = score_keeper.UpdateHighScore(current_score);

        // int last_high_score = 
        // this.game_over_screen.SetupScreen();
    }

    

    /**
    The target needs to me a meteoroid for this function to work 
    */
    // void FindAndDropMeteoroidFromList(GameObject target)
    // {
        // int drop_index;
        // target_script = target.GetComponent<RunnerMeteoroidPoints>();
        // target_id = target_script.GetUniqueId();

        // for (int i = 0; i < this.collision_list.Count; i++)
        // {
        //     list_scipt = collision_list[i].GetComponent<RunnerMeteoroidPoints>();
        //     if (target_id == list_script.GetUniqueId())
        //     {
        //         drop_index = i;
        //         break;
        //     }
        // }

    // }


    /**
        if we want a different screen for success I'll need this
        that said, the runner game is the only one that doesn't operate on a timer so...
        we'll see if we want it
    */
    void gameWinProcess()
    {
        // this.movement_speed = 0;
        
    }

    /**
    Go through the collision list (note this is a currently colliding with list)
     and check for a meteoroid that hasn't had damage applied yet
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
            if (damage > 0)
            {
                return damage;
            }

        }
        return 0;

    }
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
    

    public int GetMaxLives()
    {
        return this.MAX_LIVES;
    }

    public int GetLives()
    {
        return this.lives;
    }
}
