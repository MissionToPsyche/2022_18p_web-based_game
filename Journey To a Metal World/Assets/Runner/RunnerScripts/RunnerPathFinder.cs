using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
    This script is responsible for moving the meteoroids and is attached to each 
    meteoroid spawned
    Attached to every meteoroid prefab spawned
*/
public class RunnerPathFinder : MonoBehaviour
{
    WaveConfigSO wave_config;
    RunnerEnemySpawner enemy_spawner;
    List<Transform> waypoints;
    RunnerMeteoroidMove meteoroid_move;
    [SerializeField] float speed;
    [SerializeField] float rawSpeed;
    // Rigidbody2D rigid_body;
    int waypoint_index = 0; // so that we start at the first waypoint    
    float percent = 0f;
    Vector3 init_position;

    // happens once the instant it is created
    void Awake() 
    {
        this.enemy_spawner = FindObjectOfType<RunnerEnemySpawner>();
        // we need the spawner before we can do the stuff in start
    }

    // happens at the start
    void Start() 
    {
        wave_config = enemy_spawner.GetCurrentWave();
        waypoints = wave_config.GetWaypoints();
        transform.position = waypoints[waypoint_index].position; 
        // the 3 lines above are to get the meteoroid to know where it 
        // needs to go and where it should be right now


        this.meteoroid_move = FindObjectOfType<RunnerMeteoroidMove>();
        // need meteoroid move to know where 
        this.init_position = transform.position;
    }


    void Update() 
    {
        FollowPath();
        // notes for any future edits: 
        // using velocity is a bad idea because the orbiter does not use velocity and so if
        // anything hits it, the orbiter will have a physics reaction and go flying and become 
        // uncontrollable. Unless you are willing to reprogram the movement and physics of both the 
        // meteoroids and the orbiter, I do not think velocity based movement can be done 
        
        // Using LERP may work but it is inconvenient and may be hard. First the camera view
        // actually has a very small area so the LERP would need to increase by particularly small
        // amounts (probably). Beyond that, I'm not sure how to get all the percents to sync up
        // with a speed that all meteoroids are supposed to be following. Additionally,  if the
        // percents are off, then the meteoroids are zooming across the screen. I did try it and 
        // couldn't figure out a way to get slow movement. 

        // theoretically it may also be possible to move it via rigid body but my attempts at that 
        // never actually moved
    }


    /**
        Responsible for actually moving the metoeorid along the path given 
    */
    void FollowPath()
    {
        
        if (waypoint_index < waypoints.Count)
        {
            Vector3 target_position = waypoints[waypoint_index].position;
            // where we are trying to go
            
            float delta = meteoroid_move.GetCurrentSpeed() * Time.deltaTime;  
            // calculate the movement, framerate adjusted. if the speed goes wrong check this line

            transform.position = Vector2.MoveTowards(transform.position, 
                target_position, delta);
            // actually change the position

            this.speed = delta; // for testing purposes, show it in the serialized field
            this.rawSpeed = meteoroid_move.GetCurrentSpeed();

            // if we passed the target position go to the next waypoint
            // although in this context, there are always just the starting waypoint
            // and the ending waypoint
            if (transform.position == target_position)
            {
                waypoint_index++;
            }
        }
        // if we've passed the all waypoints then we are off screen and can destroy the meteoroid
        else
        {
            Destroy(gameObject);
        }

    }   

}