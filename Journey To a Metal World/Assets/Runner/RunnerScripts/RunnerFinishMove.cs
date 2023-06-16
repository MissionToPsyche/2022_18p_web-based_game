using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
    controls the movement of the game finsih condition (reaching Psyche)
*/
public class RunnerFinishMove : MonoBehaviour
{
    bool moving = false;
    RunnerMeteoroidMove meteoroid_move;
    // need meteoroid Move because we want its movement speed to be a proporition of the 
    // meteoroid move speed (that way if you hit somehting it doesn't look like somehow Psyche
    // is moving towards you faster than the meteoroids)

    float SPEED_DENOMINATOR = 2;
    
     // Start is called before the first frame update
    void Start()
    {
        meteoroid_move = FindObjectOfType<RunnerMeteoroidMove>();
    }

    // Update is called once per frame
    void Update()
    {
        // if we aren't supposed to be moving yet we don't move 
        if (this.moving == false)
        {
            return;
        }
        DoMovement();

    }

    /**
        Public method to be able to tell this script it should
        start moving 
    */
    public void StartMoving()
    {
        this.moving = true;
    }

    /*
        sets member variable moving to false to stop further movement
    */
    public void StopMoving()
    {
        this.moving = false;

    }
    
    /**
        The method that actually takes care of the movement
    */
    void DoMovement()
    {
        float speed = -this.meteoroid_move.GetCurrentSpeed() / this.SPEED_DENOMINATOR ;
        // note! Because we're doing the movement completely manually and need to move left on the screen
        // we made the speed value NEGATIVE

        float x_movement = speed * Time.deltaTime; // make it frame rate independent 

        transform.Translate(x_movement, 0, 0);
        // directly change the position
    }


   
}