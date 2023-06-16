using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ray : MonoBehaviour
{
    /// <summary> This function is to destroy the ray when it detects the research area </summary>
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "research area")
        {
            Destroy(gameObject, 0.2f);
        }   
    }
}