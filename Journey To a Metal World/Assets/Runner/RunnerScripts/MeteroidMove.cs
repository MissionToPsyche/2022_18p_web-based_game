using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteroidMove : MonoBehaviour
{
    // will likely need to be able to trigger the orbital
    [SerializeField] float movement_speed = -0.001f;
    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        // intended to attach this script to the center of all obstaces
        // to move everything left across the screen at the orbital at a constant pace)
        transform.Translate(this.movement_speed, 0, 0);
        
    }
}
