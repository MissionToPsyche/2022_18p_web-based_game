using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//intended to move slowly, keeping at a rate with the meteoroid movements. 
// note this cannot be the same as finish move because this one should move from the start
public class RunnerBackgroundMove : MonoBehaviour
{
    bool moving = true;
    RunnerMeteoroidMove meteoroid_move;
    float SPEED_DENOMINATOR = 4;
    
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

    // void StartMoving()
    // {
    //     this.moving = true;
    // }

    void DoMovement()
    {
        float speed = -this.meteoroid_move.GetCurrentSpeed() / this.SPEED_DENOMINATOR ;
        float x_movement = speed * Time.deltaTime;
        // we're directly transforming this one so it'll need to move left, so let's make it negative
        transform.Translate(x_movement, 0, 0);
    }

    public void StopBackgroundMovement()
    {
        this.moving = false;
        Debug.Log("Background movement should be stopped");
    }

   
}