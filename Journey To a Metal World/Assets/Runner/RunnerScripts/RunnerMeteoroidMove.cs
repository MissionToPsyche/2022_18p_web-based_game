using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// works well enough ish for the background movement dependency / related code
//however will not work with wave. code
// may need dedicated code inside the wave runner to do the speeds for that one?
// problem is getting it to react to the orbital... unless I give it a trigger too?
// but then the bigger problem is that even if one meteoroid interacts, I wanted them
// all to slow down to give the impression that the orbital was speeding up and then
// lost speed. 
// idea...make this or some other script that moves itself transform.Translate... give it to the meteoroid spawner object. all 
// meteoroids made from it are children. by moving the parent it should affect the children. the spawner itself
// cannot hit anything (well once I remove the collider that was to be used to make sure the finish line would
// not conflict with it.)
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
