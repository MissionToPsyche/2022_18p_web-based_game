using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseBOrbitalMovement : MonoBehaviour
{
    [SerializeField] Transform start;
    [SerializeField] Transform middle;
    [SerializeField] Transform end;

    private float interpolationRatio;

    // Update is called once per frame
    void Update()
    {
        interpolationRatio = (interpolationRatio + (Time.deltaTime / 4)) % 1;

        Vector3 first = Vector3.Lerp(start.position, middle.position, interpolationRatio);
        Vector3 second = Vector3.Lerp(middle.position, end.position, interpolationRatio);
        transform.position = Vector3.Lerp(first, second, interpolationRatio);
    }
}
