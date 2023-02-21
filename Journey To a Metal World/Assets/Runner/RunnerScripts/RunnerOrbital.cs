using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RunnerOrbital : MonoBehaviour
{
    int lives = 3;
    float power_bank = 100f;
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
        
    }

    // intended to work with movement asset. 
    void OnOrbitalMovement(InputValue value)
    {

    }
}
