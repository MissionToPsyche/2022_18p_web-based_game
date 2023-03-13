using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerMeteoroidMove : MonoBehaviour
{
    // will likely need to be able to trigger the orbital
    [SerializeField] float MIN_MOVEMENT_SPEED = -0.01f;
    [SerializeField] float current_movement_speed = -0.01f;
    [SerializeField] float MAX_MOVEMENT_SPEED = -0.5f;
    [SerializeField] float SPEED_INCREASE = -0.01f;
    
    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        // intended to attach this script to the center of all obstaces
        // to move everything left across the screen at the orbital at a constant pace)
        this.current_movement_speed += this.SPEED_INCREASE;
        float temp = System.Convert.ToSingle(Time.deltaTime);
        float movement = this.current_movement_speed * temp;
        // probably got to do that in reverse actually because the MAX movement speed is more negative. 
        movement = Mathf.Clamp(movement, this.MAX_MOVEMENT_SPEED, this.MIN_MOVEMENT_SPEED);

        transform.Translate(movement, 0, 0);
        // transform.Translate(this.movement_speed, 0, 0);
        
    }

    public void ResetSpeed()
    {
        this.current_movement_speed = this.MIN_MOVEMENT_SPEED;
    }
}
