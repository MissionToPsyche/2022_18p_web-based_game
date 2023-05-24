using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerCameraShake : MonoBehaviour
{

    [SerializeField] float shake_duration = 0.5f; 
    [SerializeField] float shake_magnitude = 0.1f;

    Vector3 INITIAL_POSITION; 

    // Start is called before the first frame update
    void Start()
    {
        this.INITIAL_POSITION = transform.position;
    }

    /**
    starts the coroutine
    */
    public void PlayCameraShake()
    {
        StartCoroutine(Shake());
    }

    /**
    shakes the camera by changing the camera's position to be somewhere else inside a 
    circle that is centered on the original camera position
    doing it fast enough gives you a camera shake 
    */
    IEnumerator Shake()
    {
        float elapsed_time = 0;
        while(elapsed_time < this.shake_duration)
        {
            transform.position = this.INITIAL_POSITION + (Vector3)Random.insideUnitCircle * this.shake_magnitude;
            elapsed_time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = this.INITIAL_POSITION;
    }

}
