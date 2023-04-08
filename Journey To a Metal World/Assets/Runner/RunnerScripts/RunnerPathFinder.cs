using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// brainstorming note:
// hmm consider for points. if I can't increase points when they pass immediately.
// then what about when they come off the screen and get destroyed? make it so 
// each meteoroid has a point value and a collision reduces it to 0... and loss of point 
// for current points. or we divide by 3 or something like that otherwise too many points?
// or I let an entire wave be worth 1 point
// note, needed to add this to the literal meteoroid component to get it to move. 
// may produce problems with multiple paths, check more later
public class RunnerPathFinder : MonoBehaviour
{
    WaveConfigSO wave_config;
    RunnerEnemySpawner enemy_spawner;
    List<Transform> waypoints;
    int waypoint_index = 0; // so that we start at the first waypoint    

    void Awake() 
    {
        enemy_spawner = FindObjectOfType<RunnerEnemySpawner>();
        // print(enemy_spawner);    
    }

    void Start() 
    {
        wave_config = enemy_spawner.GetCurrentWave();
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
            float delta = wave_config.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, 
                target_position, delta);
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