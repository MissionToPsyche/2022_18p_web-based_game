using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Orbit : MonoBehaviour
{
    double radius = 5;
    [SerializeField] double degree;
    [SerializeField] float delta_radius = 0.005f;
    [SerializeField] float delta_speed = 0.04f;
    int iter_count = 0;
    public static float speed = 0.1f;
    
    void Start()
    {
        transform.position = new Vector3(0f, 0f, 0);
        degree = 0.0;
        speed = 0.15f;
        iter_count = 0;        
    }

    void Update()
    {
        if(StartScene.game_start == true){
            UpdatePosition();
            SpeedUp();
            StopOrbit();
        }
    }

    void UpdatePosition()
    {
        double angle = degree * (Math.PI / 180);
        float y = (float) (radius * Math.Sin(angle));
        float x = (float) (radius * Math.Cos(angle));
        float delta_x = x - transform.position.x;
        float delta_y = y - transform.position.y;
        transform.Translate(delta_x, delta_y, 0);
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
