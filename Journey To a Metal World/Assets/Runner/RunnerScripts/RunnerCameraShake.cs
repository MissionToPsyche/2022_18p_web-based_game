using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
    Responsible for the camera shaking that happens when there are collisions (that do damage)
    with meteoroids
    Should be attached to one object that does not get destroyed. The logical choice is to
    put it on the camera object
*/
public class RunnerCameraShake : MonoBehaviour
{

    [SerializeField] float shake_duration = 0.5f; 
    // how long the screen shakes (seconds)

    [SerializeField] float shake_magnitude = 0.1f;
    // how much the screen shakes

    Vector3 INITIAL_POSITION; 

    // Start is called before the first frame update
    void Start()
    {
        this.INITIAL_POSITION = transform.position;
        // set up the initial position of the camera
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
        This is done in a coroutine because we constantly need different numbers to shake fast enough to make it
        a screenshake rather than jagged jumps in movement
    */
    IEnumerator Shake()
    {
        float elapsed_time = 0;
        while(elapsed_time < this.shake_duration)
        {
            transform.position = this.INITIAL_POSITION + (Vector3)Random.insideUnitCircle * this.shake_magnitude;
            // find the new position
            elapsed_time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = this.INITIAL_POSITION;
    }

}
