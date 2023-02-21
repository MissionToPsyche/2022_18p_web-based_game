using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RunnerOrbital : MonoBehaviour
{
    int lives = 3;
    float power_bank = 100f;
    float movement_speed = 0.5f;
    Vector2 raw_input;

    // public input
    // deing each update probably subtrat from this value if
    // movements keys are being pressed. otherwise increase this value 
    // Start is called before the first frame update


    //will need to react to triggers 

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // moving the orbital
        OrbitalMovement();
    }

    void OrbitalMovement()
    {
        //Distance per second  = units moving per frame * how many frames per second  * how many seconds per frame
        Vector3 change = raw_input * movement_speed * Time.deltaTime;
        transform.position += change;
    }

    // intended to work with movement asset. 
    void OnMove(InputValue value)
    {
        //should be getting the result from the input to w and s keys
        raw_input = value.Get<Vector2>();
        Debug.Log("move value is " + raw_input);
    }
}
