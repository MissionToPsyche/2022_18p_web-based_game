using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Rotating : MonoBehaviour
{
    public float speed = Orbit.speed;
    private Transform transform_object;
    private void Start()
    {
        transform_object = transform;
        StartCoroutine(MoveObject());
    }
    private IEnumerator MoveObject()
    {
        while(true){
            if(StartScene.game_start == true)
            {
                speed = Orbit.speed;
                transform_object.Rotate(new Vector3(0, 0, speed));
            }
            yield return null;
        }
    }
    // void Update()
    // {
    //     if(StartScene.game_start == true){
    //         speed = Orbit.speed;
    //         transform.Rotate(new Vector3(0, 0, speed));
    //     }
    // }

}
