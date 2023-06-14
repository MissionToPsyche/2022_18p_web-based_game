using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
    Responsible for moving the small orbiter in the progress bar
    at the bottom of the screen
    needs to be attached to the mini orbital in the progress bar
*/
public class RunnerProgressBar : MonoBehaviour
{
    [SerializeField] Transform init_object_position;
    // give it an object whose Transform will be used as the inital object position

    [SerializeField] Transform target_object_position;
    // give it an object whose Transform will be used as the target object position

    // As of now, they are titled: Progress Start Point and Progress End Point in the Unity Editor

    /**
        is the function responsible for moving the minit orbiter (the object this script should be attached to)
        towards the end position. 
    */
    public void ProgressBarLERP(float percent)
    {
        transform.position = Vector3.Lerp(this.init_object_position.position, this.target_object_position.position, percent);
        // with the initial_object_position as the start poitn and the target_object_position as the
        // end point. move the object to be at a position equal to @percent of the way there
    }

}