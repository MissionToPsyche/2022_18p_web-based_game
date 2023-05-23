using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// brainstorming note:


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
        this.init_position = transform.position;
        // rigid_body = transform.GetChild(0).gameObject.GetComponent<Rigidbody2D>(); 
        // Debug.Log(rigid_body);
    }

    // void FixedUpdate()
    // {
    //     FollowPath();
    // }

    void Update() 
    {
        FollowPath();
        // ApplyVelocity();
        // DoLerp();
    }

    void ApplyVelocity()
    {
        // bad idea. orbital has no velocity. getting hit makes it uncontrollable
        // Vector2 vector= new Vector2(-1, 0);
        // Vector2 velocity = vector* meteoroid_move.GetCurrentSpeed() * Time.deltaTime;
        // rigid_body.MovePosition(rigid_body.position + velocity);
    }

    void DoLerp()
    {
        Vector3 target_pos = waypoints[1].position; // this is the end waypoint that is off screen
        // float speed = 0.01f;
        this.percent += meteoroid_move.GetCurrentSpeed()*Time.deltaTime;
        // float speed = meteoroid_move.GetCurrentSpeed()*Time.deltaTime;
        // this.percent += .001f * Time.deltaTime;
        // this.percent += 0.001f;
        // transform.position = Vector3.Lerp(init_position, target_pos, this.percent);
        // transform.position = Vector3.Lerp(init_position, target_pos, 1f - (1f/ (speed* Time.deltaTime) + 1f));
        transform.position = Vector3.Lerp(init_position, target_pos, this.percent);

        if (transform.position == target_pos)
        {
            Destroy(gameObject);
        }

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