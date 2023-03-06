using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Rotating : MonoBehaviour
{
    public float speed = Orbitting.speed;
    void Update()
    {
        speed = Orbitting.speed;
        transform.Rotate(new Vector3(0, 0, speed));
    }

}
