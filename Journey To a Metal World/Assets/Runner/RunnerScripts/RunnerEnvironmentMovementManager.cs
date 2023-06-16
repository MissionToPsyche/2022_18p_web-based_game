using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
    Controls all the different movement and spawning decisions starting and stopping
    (background, finish condition, meteoroids) 
    from one place. Intended to try to decrease the amount of files that need to be connected to each other.
    This should be attached to one invisible object
*/
public class RunnerEnvironmentMovementManager : MonoBehaviour
{
    // RunnerBackgroundMove background; // background speed control 
    // actually in the end product, the background move speed controls are closely
    // linked to the meteoroid movement and so are unnescessary in this file.
    // for the sake of a future dev knowing why it would to break the pattern and not exist
    // in this, I will leave it here but commented out

    RunnerFinishMove finishline; // finish condition movement
    RunnerEnemySpawner spawner; // meteoroid spawner
    RunnerBackgroundSpawner background_spawner; // background spawner
    RunnerMeteoroidMove meteoroid_move; // meteoroid speed control
    RunnerInitialBackgroundMove initial_background; // first background movement control
    
    bool isWin = false;


     // Start is called before the first frame update
     /**
        obtain all the necessary references
     */
    void Start()
    {
        // this.background = FindObjectOfType<RunnerBackgroundMove>();
        this.finishline = FindObjectOfType<RunnerFinishMove>();
        this.spawner = FindObjectOfType<RunnerEnemySpawner>();
        this.background_spawner = FindObjectOfType<RunnerBackgroundSpawner>();
        this.meteoroid_move = FindObjectOfType<RunnerMeteoroidMove>();
        this.initial_background = FindObjectOfType<RunnerInitialBackgroundMove>();
    }


    /*
        Start the game by turning on the background spawner, and the meteoroid
        spawner. Additionally, allow the meteoroids and backgrounds to start 
        moving (and pick up speed)    
    */
    public void BeginGame()
    {
        this.background_spawner.SpawnBackground();
        // Debug.Log("background should be able to spawn");

        // this.background.StartBackgroundMovement();
        // Debug.Log("background Movement should be enabled");
        // because background movement is actually directly
        // linked to the meteoroid movement (since it is proportionate, 
        // the startBackgroundMovement function is actually useless and 
        // was left out)

        this.meteoroid_move.StartMeteoroidMove();
        // Debug.Log("Meteoroid movement enabled");

        this.spawner.StartSpawning();
        // Debug.Log("Meteoroid spawning enabled");

        this.initial_background.StartMoving();

    }


    /**
        Stops the finish area from moving offscreen by stopping it once 
        it reaches the object with this script attached to it
    */
     void OnTriggerEnter2D(Collider2D other) {
        // if we enter the finish trigger . we stop it from moving any further
        // also then we have a win 
        if (other.tag == "Finish")
        {
            this.finishline.StopMoving();
            Debug.Log("should have stopped moving finish");
            this.isWin = true;
        }   

        // this tag won't arrive
        else if (other.tag == "StopBackground")
        {
            // this.background.StopBackgroundMovement(); // unless I start using the background move script again
            // this specific line is pointless. 
            // as currently it is a fraction of meteoroid move speed
            this.meteoroid_move.StopMeteoroidMovement();
            Debug.Log("should have stopped moving background");
        }
    }

    /**
        getter function for whether or not the game has been won
    */
    public bool GetIsWin()
    {
        return this.isWin;
    }    

    /**
        Method made that stops the movement of meteoroids and the background 
    */
    public void StopBackgroundMovement()
    {
        // this.background.StopBackgroundMovement();
        this.meteoroid_move.StopMeteoroidMovement();
        // stops meteoroids from moving and by extension stops the background movement 
        // since it's supposed to be proportionate to the meteoroids

        this.initial_background.StopMoving();
        // this runs on a different system compared to all other backgrounds so it needs to
        // be addressed specifically.
        // Debug.Log("should have stopped moving background");
    }

    
   
}