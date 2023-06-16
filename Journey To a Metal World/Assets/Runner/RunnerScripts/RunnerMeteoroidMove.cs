using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
    The overhead that calculates the raw values that the meteoroids should be moving at
    this should be attached to one invisible object in the game 
*/
public class RunnerMeteoroidMove : MonoBehaviour
{
    [SerializeField] float MIN_MOVEMENT_SPEED = 5f;
    [SerializeField] float current_movement_speed = 10f;
    [SerializeField] float MAX_MOVEMENT_SPEED = 20f;

    [SerializeField] float SPEED_INCREASE = 0.01f;
    bool game_over = false;
    bool game_started = false;

    // Update is called once per frame
    void Update()
    {
        // if the game hasn't started we don't do anything
        if (this.game_started == false)
        {
            return;
        }
        
        // if the game is over we also don't do anything
        if (this.game_over == true)
        {
            return;
        }
        // else we increase the speed the meteoroids are moving at 
        IncreaseSpeed();
        
    }

    /**
        Public method for setting the game_started member variable to true
        this will let the meteoroidMove script know that it should start increasing the
        move speed
    */
    public void StartMeteoroidMove()
    {
        this.game_started = true;
    }

    /**
        Increments the speed by the SPEED_INCREASE constant and keeps it within minimum and maximum bounds
    */
    public void IncreaseSpeed()
    {
        // intended to have this file control the speed calculations for all.
        // if we're already at or faster than the current max speed we just return 
        if (this.current_movement_speed >= MAX_MOVEMENT_SPEED)
        {
            return; 
        }
        this.current_movement_speed += this.SPEED_INCREASE;
    }

    /**
        Reset the movement speed to be the minimum movement speed
    */
    public void ResetSpeed()
    {
        this.current_movement_speed = this.MIN_MOVEMENT_SPEED;
    }

    /**
        Stops all meteoroid movements. Also sets game_over to true. 
    */
    public void StopMeteoroidMovement()
    {
        this.current_movement_speed = 0;
        this.game_over = true;
    }
    /**
        Getter for the current (raw) speed of the meteoroids on screen
    */
    public float GetCurrentSpeed()
    {
        return this.current_movement_speed;
    }
}
