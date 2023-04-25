using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// brainstorming note:

// need to test and make sure everything transfers over right. 
// may just do the same manual style as before?

public class RunnerBackgroundPathFinder : MonoBehaviour
{
    [SerializeField] BackgroundWaveConfigSO wave_config;
    RunnerBackgroundSpawner background_spawner;
    List<Transform> waypoints;
    RunnerBackgroundMove background_move;
    [SerializeField] float speed;
    [SerializeField] float rawSpeed;
    int waypoint_index = 0; // so that we start at the first waypoint    

    void Awake() 
    {
        this.background_spawner = FindObjectOfType<RunnerBackgroundSpawner>();
        this.background_move = FindObjectOfType<RunnerBackgroundMove>();

    }

    void Start() 
    {
        // wave_config = this.background_spawner.GetCurrentWave();
        waypoints = wave_config.GetWaypoints();
        transform.position = waypoints[waypoint_index].position; 

    }


    void Update() 
    {
        FollowPath();
    }

    void FollowPath()
    {
        if (waypoint_index < waypoints.Count)
        {
            Vector3 target_position = waypoints[waypoint_index].position;
            // float delta = wave_config.GetMoveSpeed() * Time.deltaTime;
            float delta = background_move.GetRawMovementSpeed() * Time.deltaTime;  // somehow this
            //doesn't seem to impact the speed?
            transform.position = Vector2.MoveTowards(transform.position, 
                target_position, delta);
            this.speed = delta; // for testing purposes, trying to figure out speed...
            this.rawSpeed = background_move.GetRawMovementSpeed();
            if (transform.position == target_position)
            {
                waypoint_index++;
            }
        }
        else
        {
            Destroy(gameObject);
        }

    }   

}