using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// correction, seems to work with the pathfinder code. possible that before my speeds just weren't
//  high enough to be noticeable
public class RunnerMeteoroidMove : MonoBehaviour
{
    // will likely need to be able to trigger the orbital
    [SerializeField] float MIN_MOVEMENT_SPEED = 5f;
    [SerializeField] float current_movement_speed = 10f;
    [SerializeField] float MAX_MOVEMENT_SPEED = 20f;
    // speeds attempted: 10, 20 
    // speed increase attempted 0.01. 

    // attempt: try it with changed condition so that the inspector isn't changing every frame once
    // it hits max speed. if that doesn't work. we just make it a consistent 20 or 15. 
    // 0.001 speed increase is slower although kinda more forgiving. s
    [SerializeField] float SPEED_INCREASE = 0.01f;
    bool game_over = false;
    bool game_started = false;
    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        if (this.game_started == false)
        {
            return;
        }
        
        if (this.game_over == true)
        {
            return;
        }
        IncreaseSpeed();
        // transform.Translate(movement, 0, 0);
        // transform.Translate(this.movement_speed, 0, 0);
        
    }

    public void StartMeteoroidMove()
    {
        this.game_started = true;
        // this.current_movement_speed = 20;
    }
    /**
        Increments the speed by the SPEED_INCREASE constant and keeps it within minimum and maximum bounds
    */
    public void IncreaseSpeed()
    {
        // intended to have this file control the speed calculations for all.
        // this.current_movement_speed += this.SPEED_INCREASE;
        if (this.current_movement_speed >= MAX_MOVEMENT_SPEED)
        {
            return;
        }
        this.current_movement_speed += this.SPEED_INCREASE;
        // this.current_movement_speed = Mathf.Clamp(this.current_movement_speed + this.SPEED_INCREASE, this.MAX_MOVEMENT_SPEED, this.MIN_MOVEMENT_SPEED);
    }

    public void ResetSpeed()
    {
        this.current_movement_speed = this.MIN_MOVEMENT_SPEED;
    }

    public void StopMeteoroidMovement()
    {
        this.current_movement_speed = 0;
        this.game_over = true;
    }
    /**
    returns the current speed of the meteoroids on the screen
    */
    public float GetCurrentSpeed()
    {
        return this.current_movement_speed;
    }
}
