using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void StartMoving()
    {
        this.moving = true;
    }

    void DoMovement()
    {
        float speed = -this.meteoroid_move.GetCurrentSpeed() / this.SPEED_DENOMINATOR ;
        float x_movement = speed * Time.deltaTime;
        // we're directly transforming this one so it'll need to move left, so let's make it negative
        transform.Translate(x_movement, 0, 0);
    }


   
}