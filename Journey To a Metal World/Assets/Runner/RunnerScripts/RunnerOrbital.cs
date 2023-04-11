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
    int lives;
    // float power_bank = 100f;
    float movement_speed = 2f;
    float frames_per_second;
    int INVINCIBILITY_FRAME_LENGTH;
    int invinciblity_frames_left = 0;
    int current_collisions = 0; // because we can theoretically hit multiple things at once
    bool damage_effect_on = false;

    Vector2 raw_input;
    SpriteRenderer sprite;
    Color orbital_color;
    RunnerScore score_keeper;
    bool game_over = false;
    bool game_won = false;
    int COLLISION_MINUS_POINT = -1;
    RunnerMeteoroidMove stage; 

    Vector2 min_bounds;
    Vector2 max_bounds;
    [SerializeField] float padding_left = 1.5f;
    [SerializeField] float padding_right = 1.5f;
    [SerializeField] float padding_top = 2.1f;
    [SerializeField] float padding_bottom = 0.9f;
    RunnerLivesFrontEndManager visual_manager;
    
    // Color flash = ThisSprite.GetComponent<SpriteRenderer>().color;
    // flash.

    void Start()
    {
        this.frames_per_second = 1/Time.deltaTime;
        this.INVINCIBILITY_FRAME_LENGTH = (int)frames_per_second * 2;
        this.sprite = GetComponent<SpriteRenderer>();
        this.orbital_color = sprite.color;
        this.score_keeper = FindObjectOfType<RunnerScore>();
        this.stage = FindObjectOfType<RunnerMeteoroidMove>();
        this.visual_manager = FindObjectOfType<RunnerLivesFrontEndManager>();
        this.lives = MAX_LIVES;
        InitBounds();

    }

    // Update is called once per frame
    void Update()
    {
        // moving the orbital
        // need to 
        OrbitalMovement();
        DamageApplied(this.current_collisions);
        if (this.invinciblity_frames_left > 0)
        {
            this.invinciblity_frames_left--; 
        }

        if (this.game_over == true)
        {
            if (this.game_won == false)
            {
                gameLostProcess(); 
            }
            else
            {
                
            }
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

    void OrbitalMovement()
    {
        Vector2 delta = raw_input * movement_speed * Time.deltaTime;;
        Vector2 new_position = new Vector2();
        new_position.x = Mathf.Clamp(transform.position.x + delta.x, this.min_bounds.x + this.padding_left, 
            this.max_bounds.x - this.padding_right);
        new_position.y = Mathf.Clamp(transform.position.y + delta.y, this.min_bounds.y + this.padding_bottom, 
            this.max_bounds.y - this.padding_top);
        transform.position = new_position;
    }


    // intended to work with movement asset. 
    void OnMove(InputValue value)
    {
        //should be getting the result from the input to w and s keys
        this.raw_input = value.Get<Vector2>();
        Debug.Log("move value is " + this.raw_input);
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
        if (other.tag == "Finish" && this.game_over == false)
        {
            this.game_over = true;
            this.game_won = true;
            Debug.Log("Finish Line passed");
        }    
        else if (other.tag == "PointBox")
        {
            RunnerMeteoroidPoints pointManager = other.GetComponent<RunnerMeteoroidPoints>();
            pointManager.ReachedPointsArea();   
        }

    }

    // void OnTrig

    /**
    when we collide with something we increase the count of how many things we are currently
    colliding with. 
    Collision2D other is representing the thing we collided with in case we need info from it
    */
    void OnCollisionEnter2D(Collision2D other)
    {
        this.current_collisions++;
        other.gameObject.transform.BroadcastMessage("SetPointTotalToZero");
        stage.ResetSpeed();
        //note by default. it's per collision. also not exit collision. 
        // use enter and exit to bound the invincibility? 
        // include a timer of some sort otherwise explot. push meteroid to end
        // if ((other.gameObject.CompareTag("Finish") == true) && this.game_over == false)
        // if (this.game_over == false)
        // {
        //     this.game_over = true;
        //     this.game_won = true;
        //     Debug.Log("Finish Line passed");
        // }
        // else
        // {
            // this.current_collisions++;

        // }
        // if (this.invinciblity_frames_left = 0)
        // {
        //     this.lives--;
        //     this.invinciblity_frames_left = INVINCIBILITY_FRAME_LENGTH;
        //     Debug.Log("Took a hit. number of lives left is: " + this.lives);
        // }
    }

    /**
    when we stop colliding with something we decrease the count of how many things we are currently
    colliding with. By implementation, this number should never go below 0 
    Collision2D other is representing the thing we collided with in case we need info from it
    */
    void OnCollisionExit2D(Collision2D other) 
    {
        // if (other.gameObject.CompareTag("Finish") == false)
        // if (other.tag != "Finish")
        // {
        //     this.current_collisions--;    
        // }
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
            Debug.Log("has " + this.invinciblity_frames_left + " invinciblity frames left. no lives lost. \n Lives left = " + this.lives);
            if (this.invinciblity_frames_left % ((int)(this.INVINCIBILITY_FRAME_LENGTH/2)) == 0)
            {
                // orbital_color.a = 0;
                if (damage_effect_on == true)
                {
                    this.damage_effect_on = false;
                    orbital_color.a = 0;
                    sprite.color = orbital_color;
                    // sprite.color.a = 0;
                    Debug.Log("orbital should be invisible");
                }
                else if (damage_effect_on == false)
                {
                    this.damage_effect_on = true;
                    orbital_color.a = 255;    
                    sprite.color = orbital_color;
                    Debug.Log("orbital is visible");

                }
            }
            return 0;
        }
        //so if it's here then there was 0 invincibility frames
        this.damage_effect_on = false;
        
        orbital_color.a = 255;
        sprite.color = orbital_color;
        if (collision_count > 0)
        {
            this.damage_effect_on = true;
            DecreaseLives();
            score_keeper.AddToScore(this.COLLISION_MINUS_POINT);

            Debug.Log("No invincibility frames left. -1 life. \n Lives left = " + this.lives);
            this.invinciblity_frames_left = this.INVINCIBILITY_FRAME_LENGTH;
            if (this.lives == 0)
            {
                this.game_over = true;
                this.game_won = false;
                orbital_color.a = 255;
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
        this.orbital_color.a--;
        this.sprite.color = orbital_color;
        if (orbital_color.a == 0)
        {
            return true;
        }
        return false;
    }

    void DecreaseLives()
    {
        this.lives--;
        this.visual_manager.DecreaseLifeSprite();
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
