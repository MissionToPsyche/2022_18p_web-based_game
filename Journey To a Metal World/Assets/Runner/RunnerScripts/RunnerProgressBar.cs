using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerProgressBar : MonoBehaviour
{
    [SerializeField] Transform init_object_position;
    [SerializeField] Transform target_object_position;
    // Vector3 init_position;
    // Vector3 target_position;
    // int progress
    void start()
    {
        // this.init_position = init_object_position.GetComponent<Transform>().position;
        // this.target_position = target_object_position.GetComponent<Transform>().position;
        // Debug.Log(init_position);
        // Debug.Log(target_position);
    } 

    public void ProgressBarLERP(float percent)
    {
        transform.position = Vector3.Lerp(this.init_object_position.position, this.target_object_position.position, percent);
        // transform.position = Vector3.Lerp(transform.position, this.target_object_position.position, percent);

        // transform.position = Vector3.Lerp(this.init_position, this.target_position, percent);
        // Debug.Log(transform.position);
        // Debug.Log(this.init_position);
        // Debug.Log(this.target_position);

        
    }

}