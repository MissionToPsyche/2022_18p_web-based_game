using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
    The first initial background in the scene needs slightly different 
    controls but the general idea of its movement is pretty similar 
    to the generic background movement 
    Needs to be attached to the inital background
*/
public class RunnerInitialBackgroundMove : MonoBehaviour
{
    RunnerBackgroundMove background_move; 
    [SerializeField] GameObject target; 

    Vector3 target_position;
    bool moving = false;
    
    /**
        Set up references to what will be telling us how fast it should be moving and where
    */
    void Start()
    {
        this.background_move = FindObjectOfType<RunnerBackgroundMove>();
        this.target_position = target.gameObject.GetComponent<Transform>().position;
    }

    /**
        Called every frame. In this case takes care of the movement 
    */
    void Update()
    {
        // if we're not supposed to be moving yet we do nothing 
        if (true == this.moving)
        {
            DoMovement(); // note, since the initial background movement is set to 0, having it like this is safe at the start
        }
    }

    /**
        We start allowing the background to move
    */
    public void StartMoving()
    {
        this.moving = true;
    }

    /**
        We stop the background from moving any more
        If the overall meteoroid movement speed was reduced to 0 that would 
        also effectively do the same thign even if this.moving was still true
    */
    public void StopMoving()
    {
        this.moving = false;
    }

    /**
        This method actually does the movement calculations towards the target postion
        and destroys the object the script is attached to once it reaches it
    */
    void DoMovement()
    {
        float delta = background_move.GetRawMovementSpeed() * Time.deltaTime;
        // intended to be frame rate independent movement based off of the universal background movement speed
        transform.position = Vector2.MoveTowards(transform.position, target_position, delta);
        if (transform.position == target_position)
        {
            Destroy(gameObject);
        }

    }
}