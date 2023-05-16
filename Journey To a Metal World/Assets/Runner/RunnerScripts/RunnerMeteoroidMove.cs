using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// correction, seems to work with the pathfinder code. possible that before my speeds just weren't
//  high enough to be noticeable
public class RunnerMeteoroidMove : MonoBehaviour
{
    // will likely need to be able to trigger the orbital
    [SerializeField] float MIN_MOVEMENT_SPEED = 0.01f;
    [SerializeField] float current_movement_speed = 0.01f;
    [SerializeField] float MAX_MOVEMENT_SPEED = 20f;
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
    }
    /**
        Increments the speed by the SPEED_INCREASE constant and keeps it within minimum and maximum bounds
    */
    public void IncreaseSpeed()
    {
        // intended to have this file control the speed calculations for all.
        // this.current_movement_speed += this.SPEED_INCREASE;
        this.current_movement_speed = Mathf.Clamp(this.current_movement_speed + this.SPEED_INCREASE, this.MAX_MOVEMENT_SPEED, this.MIN_MOVEMENT_SPEED);
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
