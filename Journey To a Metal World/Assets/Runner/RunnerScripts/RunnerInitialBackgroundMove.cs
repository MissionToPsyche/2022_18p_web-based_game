using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerInitialBackgroundMove : MonoBehaviour
{
    RunnerBackgroundMove background_move; 
    [SerializeField] GameObject target;
    Vector3 target_position;
    bool moving = true;
    void Start()
    {
        this.background_move = FindObjectOfType<RunnerBackgroundMove>();
        this.target_position = target.gameObject.GetComponent<Transform>().position;
    }

    void Update()
    {
        if (true == this.moving)
        {
            DoMovement(); // note, since the initial background movement is set to 0, having it like this is safe at the start
        }


    }

    void DoMovement()
    {
        // Vector2 move_left = new Vector2(-1.0f, 0f);
        // float speed = background_move.GetRawMovementSpeed() * Time.deltaTime;
        float delta = background_move.GetRawMovementSpeed() * Time.deltaTime;
        // transform.Translate(move_left * speed);
        transform.position = Vector2.MoveTowards(transform.position, target_position, delta);
        if (transform.position == target_position)
        {
            Destroy(gameObject);
        }

    }
    // bugged in event of lives lost this will probably keep moving but for now it's a stub. 
    // Also note to self, need to figure out how to get the buttons to be the same size
}