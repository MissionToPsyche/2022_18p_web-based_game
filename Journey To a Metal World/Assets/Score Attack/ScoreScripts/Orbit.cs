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
    float radius = 5.0f;
    public static float speed = 0.15f;
    private Transform transform_object;

    
    void Start()
    {
        transform_object = transform;
        transform_object.position = new Vector3(0f, 0f, 0);
        degree = 0.0f;
        // below are the real deployment value
        speed = 0.8f;
        delta_radius = 0.005f;
        delta_speed = 0.3f;
        iter_count = 0;   
        StartCoroutine(UpdateOrbitMove());
    }

    /// <summary> This function is to update the position of the orbital </summary>
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


    void UpdatePosition()
    {
        float angle_radian = degree * RADIAN;
        transform_object.position = new Vector3(radius * Mathf.Cos(angle_radian), radius * Mathf.Sin(angle_radian), 0);
        degree += speed;
        iter_count += 1;
    }

    void SpeedUp()
    {
        if(iter_count % 100 == 0 && radius >= 3)
        {
            radius -= delta_radius;
        }

        if(iter_count % 1000 == 0 && radius >= 3)
        {
            speed += delta_speed;
        }
    }

    void StopOrbit()
    {
        if(EndScene.timeout){
            speed = 0.0f;
        }
    }
}
