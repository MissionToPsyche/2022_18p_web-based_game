using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// brainstorming note:

// note, needed to add this to the literal meteoroid component to get it to move. 
// may produce problems with multiple paths, check more later

// changing the speed in this file so that it increases could work... the thing is, 
// I need to find out if it can in fact take another object
// also making this comment because the merge didn't get all the files I need
// but otherwise the merge thinks everything is up to date
public class RunnerPathFinder : MonoBehaviour
{
    WaveConfigSO wave_config;
    RunnerEnemySpawner enemy_spawner;
    List<Transform> waypoints;
    RunnerMeteoroidMove meteoroid_move;
    [SerializeField] float speed;
    [SerializeField] float rawSpeed;
    Rigidbody2D rigid_body;
    int waypoint_index = 0; // so that we start at the first waypoint    

    void Awake() 
    {
        this.enemy_spawner = FindObjectOfType<RunnerEnemySpawner>();

    }

    void Start() 
    {
        wave_config = enemy_spawner.GetCurrentWave();
        waypoints = wave_config.GetWaypoints();
        transform.position = waypoints[waypoint_index].position; 
        this.meteoroid_move = FindObjectOfType<RunnerMeteoroidMove>();
        rigid_body = transform.GetChild(0).gameObject.GetComponent<Rigidbody2D>(); 
        // Debug.Log(rigid_body);
    }

    // void FixedUpdate()
    // {
    //     FollowPath();
    // }

    void Update() 
    {
        // FollowPath();
        // ApplyVelocity();
    }

    void ApplyVelocity()
    {

        // Vector2 vector= new Vector2(-1, 0);
        // Vector2 velocity = vector* meteoroid_move.GetCurrentSpeed() * Time.deltaTime;
        // rigid_body.MovePosition(rigid_body.position + velocity);
    }

    void FollowPath()
    {
        if (waypoint_index < waypoints.Count)
        {
            Vector3 target_position = waypoints[waypoint_index].position;
            
            // float delta = wave_config.GetMoveSpeed() * Time.deltaTime;
            float delta = meteoroid_move.GetCurrentSpeed() * Time.deltaTime;  // somehow this
            //doesn't seem to impact the speed?

// look into doing the movement with rigid body
            transform.position = Vector2.MoveTowards(transform.position, 
                target_position, delta);
            
            // Vector3 movement_left = new Vector3(-1.0f, 0f, 0f);

            // this.rigid_body.MovePosition(transform.position + movement_left * delta);

            this.speed = delta; // for testing purposes, trying to figure out speed...
            this.rawSpeed = meteoroid_move.GetCurrentSpeed();
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