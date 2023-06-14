using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
    This script is attached to every Background spawned into the Runner (meaning the initial background
    does not have this script. It has the Runner InitialBackgroundMove because it cares about something 
    slightly different

    This file is responsible for moving the background (which is a prefab) across the screen
     and deleting it once it goes off the screen. 

     Also, due to circumstances I'm not entirely sure I can explain, the script is not active while on
     background but still takes effect but at some sort of shorter speed. I unfortunately noticed this at
     the last minute since the game worked completely before I was running final checks. 
     The game was made for what it is currently doing so making the script actually marked active actually 
     makes the background move faster than it is supposed to be.  
*/
public class RunnerBackgroundPathFinder : MonoBehaviour
{
    [SerializeField] BackgroundWaveConfigSO wave_config;
    // wave config is used becasue that is the way I saw it and it contains the object and the path that
    // the Background will be taking 

    RunnerBackgroundSpawner background_spawner;
    List<Transform> waypoints;
    RunnerBackgroundMove background_move;

    [SerializeField] float speed;
    // how fast will it be moving. This is serialized for the ability to see the numbers easily while
    // running the game in the editor.Even if you were to set it to a value, it would be re-set to a 
    // different value within this code 

    [SerializeField] float rawSpeed;
    // the difference between rawspeed and speed:
    // speed - is supposed to be the frame adjusted version
    // raw speed is not frame adjusted

    int waypoint_index = 0; // so that we start at the first waypoint    

    /**
    We will need the background_move file referenced so we can consult it for the 
    raw speed that the background should be moving at
    */
    void Awake() 
    {
        this.background_move = FindObjectOfType<RunnerBackgroundMove>();
    }

    /**
        We need the waypoints that let us know where we start from and are going to
    */
    void Start() 
    {
        waypoints = wave_config.GetWaypoints();
        transform.position = waypoints[waypoint_index].position; 

    }

    /**
        Called every frame to move the background 
    */
    void Update() 
    {
        FollowPath();
    }

    /**
        The function that does all the calculations and actually does move the background 
    */
    void FollowPath()
    {
        // we are going through the waypoints BUT not in a while loop because this function needs to
        // move it one small time each time its called. That said, we also need to know when to stop 
        // (when we've past the last waypoint)
        if (waypoint_index < waypoints.Count)
        {
            Vector3 target_position = waypoints[waypoint_index].position;
            float delta = background_move.GetRawMovementSpeed() * Time.deltaTime;  
            // compute the frame-indpeendent movement we want to make
            
            transform.position = Vector2.MoveTowards(transform.position, 
                target_position, delta);
            // change the position of the background from the current position to a delta movement
            // towards the target position

            this.speed = delta; // for testing purposes, trying to figure out speed... we set the serialized fields
            this.rawSpeed = background_move.GetRawMovementSpeed();

            // check if we've we've reached the target position. if we have, change the target position
            // to the next one in the list
            // as a side note: currently the list should have only 2 positions. Theoretically this should still work
            // if more were added but be careful if you do change it  
            if (transform.position == target_position)
            {
                waypoint_index++;
            }
        }
        // if we've past the last waypoint then we're offscreen and can delete this background now
        else 
        {
            Destroy(gameObject);
        }

    }   

}