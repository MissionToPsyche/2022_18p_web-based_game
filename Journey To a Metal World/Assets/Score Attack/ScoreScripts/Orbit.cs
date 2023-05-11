using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Orbit : MonoBehaviour
{
    [SerializeField] float degree;
    [SerializeField] float delta_radius = 0.005f;
    [SerializeField] float delta_speed = 0.04f;
    int iter_count = 0;
    float RADIAN = (Mathf.PI / 180);
    public static float speed = 0.1f;
    private Transform transform_object;
    float radius = 5.0f;

    
    void Start()
    {
        transform_object = transform;
        transform_object.position = new Vector3(0f, 0f, 0);
        degree = 0.0f;
        speed = 0.15f;
        iter_count = 0;   
        StartCoroutine(UpdateOrbitMove());
    }


    private IEnumerator UpdateOrbitMove()
    {
        while(true){
            if(StartScene.game_start == true){
                UpdatePosition();
                SpeedUp();
                StopOrbit();
            }
            yield return null;
        }
    }

    // void Update()
    // {
    //     if(StartScene.game_start == true){
    //         UpdatePosition();
    //         SpeedUp();
    //         StopOrbit();
    //     }
    // }

    void UpdatePosition()
    {
        float angle_radian = degree * RADIAN;
        float y = radius * Mathf.Sin(angle_radian);
        float x = radius * Mathf.Cos(angle_radian);
        float delta_x = x - transform_object.position.x;
        float delta_y = y - transform_object.position.y;
        transform_object.Translate(delta_x, delta_y, 0);
        degree += speed;
        iter_count += 1;
    }

    void SpeedUp()
    {
        if(iter_count % 100 == 0 && radius >= 3)
        {
            radius -= delta_radius;
        }

        if(iter_count % 4000 == 0 && radius >= 3)
        {
            speed += delta_speed;
            // Debug.Log("speed up");

        }
    }

    void StopOrbit()
    {
        if(EndScene.timeout){
            speed = 0.0f;
        }
    }

    
}
