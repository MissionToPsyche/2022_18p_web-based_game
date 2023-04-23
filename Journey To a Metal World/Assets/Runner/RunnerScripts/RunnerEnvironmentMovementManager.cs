using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Intended to be used to call the stop movement functions for the 
// background and finish. 
// given extra time, the organization of this game would much better be 
// done if everything was routed though one or two managers rather than 
// having connections everywhere
public class RunnerEnvironmentMovementManager : MonoBehaviour
{
    RunnerBackgroundMove background;
    RunnerFinishMove finishline;
     // Start is called before the first frame update

    void Start()
    {
         this.background = FindObjectOfType<RunnerBackgroundMove>();
         this.finishline = FindObjectOfType<RunnerFinishMove>();

    }

    // Update is called once per frame
    void Update()
    {

    }


    
    // void StartMoving()
    // {
    //     this.moving = true;
    // }

     void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Finish")
        {
            this.finishline.StopMoving();
            Debug.Log("should have activated stop moving finishline");
        }   
        else if (other.tag == "StopBackground")
        {
            this.background.StopBackgroundMovement();
            Debug.Log("should have activated stop moving background");
        }
    }
   
}