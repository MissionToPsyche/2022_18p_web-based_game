using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
intended to move slowly, keeping at a rate proportionate with the meteoroid movements.
there is exactly one of these scripts attached to an invisible object in the game  

note: this class looks particularly short and like it could have been moved somewhere else
to have one less file. this is true. However, during development it took a while to figure
out that the simplest way worked best and so it got reduced to one function
It is left like this because it would be more problematic to reroute everything than to leave
it in and this way if something needs to be applied to all moving backgrounds, the function can 
be added to this one location rather than becoming part of the script that must be attatched to
every single meteoroid
*/
public class RunnerBackgroundMove : MonoBehaviour
{
    RunnerMeteoroidMove meteoroid_move;
    float SPEED_DENOMINATOR = 4f;
    
     // Start is called before the first frame update
    void Start()
    {
         meteoroid_move = FindObjectOfType<RunnerMeteoroidMove>();
         // to be proportionate to the meteoroid movement speed, we're going to
         // need to know what it is which will be a function in the RunnerMeteoroidMove 
    }


   
   /**
    returns the raw movement speed (NOT the frame adjusted number though,
    that is done when it's actually about to be used) that the background
    should have. This is created by taking the speed that the meteoroids are
    moving at and dividing it by a preset constant so that the background moves
    proporitionaly to the meteoroids. 
   */
   public float GetRawMovementSpeed()
   {
        float speed = this.meteoroid_move.GetCurrentSpeed() / this.SPEED_DENOMINATOR ;
        // note that this is just raw speed and not the actual movement so this really 
        // should never be negative
        return speed;
   }
}