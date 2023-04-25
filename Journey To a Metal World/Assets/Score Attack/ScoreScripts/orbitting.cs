using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Orbitting : MonoBehaviour
{
    double radius = 5;
    [SerializeField] double degree;
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
        // transform.Rotate(new Vector3(0, 0, 0.1f));
        // Debug.Log(transform.position.x + " " + transform.position.y);
        degree += speed;
        iter_count += 1;
    }

    void SpeedUp()
    {
        if(iter_count % 100 == 0 && radius >= 3)
        {
            radius -= 0.001;
        }

        if(iter_count % 7200 == 0 && radius >= 3)
        {
            speed += 0.03f;
            Debug.Log("speed up");

        }
    }

    void StopOrbit()
    {
        if(EndScene.timeout){
            speed = 0.0f;
        }
    }

    
}
