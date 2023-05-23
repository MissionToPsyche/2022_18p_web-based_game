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
    RunnerEnemySpawner spawner;
    RunnerBackgroundSpawner background_spawner;
    RunnerMeteoroidMove meteoroid_move;
    RunnerInitialBackgroundMove initial_background;
    bool isWin = false;
     // Start is called before the first frame update

    void Start()
    {
        this.background = FindObjectOfType<RunnerBackgroundMove>();
        this.finishline = FindObjectOfType<RunnerFinishMove>();
        this.spawner = FindObjectOfType<RunnerEnemySpawner>();
        this.background_spawner = FindObjectOfType<RunnerBackgroundSpawner>();
        this.meteoroid_move = FindObjectOfType<RunnerMeteoroidMove>();
        this.initial_background = FindObjectOfType<RunnerInitialBackgroundMove>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*
    
    */
    public void BeginGame()
    {
        this.background_spawner.SpawnBackground();
        // Debug.Log("background should be spawned");

        this.background.StartBackgroundMovement();
        // Debug.Log("background Movement should be enabled");

        this.meteoroid_move.StartMeteoroidMove();
        // Debug.Log("Meteoroid movement enabled");

        this.spawner.StartSpawning();
        // Debug.Log("Meteoroid spawning enabled");

        this.initial_background.StartMoving();



        
        // ok so either I make the instructions ok button start the game...
        // or I have this waiting to recieve... 
        // or have that other file call a public function here. 
        // that last option may be the best because otherwise update will be even more crazy and consuming
    }

     void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Finish")
        {
            this.finishline.StopMoving();
            Debug.Log("should have stopped moving finish");
            this.isWin = true;
        }   
        // this tag won't arrive
        else if (other.tag == "StopBackground")
        {
            this.background.StopBackgroundMovement(); // unless I start using the background move script again
            // this specific line is pointless. 
            // as currently it is a fraction of meteoroid move speed
            this.meteoroid_move.StopMeteoroidMovement();
            Debug.Log("should have stopped moving background");
        }
    }

    public bool GetIsWin()
    {
        return this.isWin;
    }    

    public void StopBackgroundMovement()
    {
        this.background.StopBackgroundMovement();
        this.meteoroid_move.StopMeteoroidMovement();
        this.initial_background.StopMoving();
        // Debug.Log("should have stopped moving background");
    }

    
   
}