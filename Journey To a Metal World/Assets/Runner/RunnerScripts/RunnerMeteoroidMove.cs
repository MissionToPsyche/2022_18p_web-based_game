using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerMeteoroidMove : MonoBehaviour
{
    // will likely need to be able to trigger the orbital
    [SerializeField] float MIN_MOVEMENT_SPEED = 1f;
    [SerializeField] float current_movement_speed = 1f;
    [SerializeField] float MAX_MOVEMENT_SPEED = 10f;
    [SerializeField] float SPEED_INCREASE = 0.2f;
    bool game_over = false;
    
    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        if (game_over == true)
        {
            return;
        }
        IncreaseSpeed();
        // transform.Translate(movement, 0, 0);
        // transform.Translate(this.movement_speed, 0, 0);
        
    }
    /**
        Increments the speed by the SPEED_INCREASE constant and keeps it within minimum and maximum bounds
    */
    public void IncreaseSpeed()
    {
        // intended to have this file control the speed calculations for all.
        this.current_movement_speed += this.SPEED_INCREASE;
        this.current_movement_speed = Mathf.Clamp(this.current_movement_speed, this.MAX_MOVEMENT_SPEED, this.MIN_MOVEMENT_SPEED);
    }

    public void ResetSpeed()
    {
        this.current_movement_speed = this.MIN_MOVEMENT_SPEED;
    }

    public void StopMovement()
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
