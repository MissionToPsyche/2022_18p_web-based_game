using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// need to pick condition to stop it from moving. likely may involve setting up a trigger 
// for it to hit. for that matter may be using a trigger to tell the background when to stop moving.
// ... may need to spawn backgrounds if it gets long enough. 
// and then get into writing out the randomizing aspect at some point
// mqy need to change steering speed though if orbital feels like it's moving slow
public class RunnerFinishMove : MonoBehaviour
{
    bool moving = false;
    RunnerMeteoroidMove meteoroid_move;
    float SPEED_DENOMINATOR = 2;
    
     // Start is called before the first frame update
    void Start()
    {
        meteoroid_move = FindObjectOfType<RunnerMeteoroidMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.moving == false)
        {
            return;
        }
        DoMovement();

    }

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

    void DoMovement()
    {
        float speed = -this.meteoroid_move.GetCurrentSpeed() / this.SPEED_DENOMINATOR ;
        float x_movement = speed * Time.deltaTime;
        // we're directly transforming this one so it'll need to move left, so let's make it negative
        transform.Translate(x_movement, 0, 0);
    }


   
}