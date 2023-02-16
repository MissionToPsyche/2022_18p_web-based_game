using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class orbitting : MonoBehaviour
{
    int radius = 5;
    [SerializeField] double degree;
    

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(5, 0, 0);
        degree = 0.0;
    }

    // Update is called once per frame
    void Update()
    {
        updatePosition();
    }

    void updatePosition()
    {
        double angle = degree * (Math.PI / 180);
        float y = (float) (radius * Math.Sin(angle));
        float x = (float) (radius * Math.Cos(angle));
        float delta_x = x - transform.position.x;
        float delta_y = y - transform.position.y;
        transform.Translate(delta_x, delta_y, 0);
        degree += 0.1;
    }

    
}
