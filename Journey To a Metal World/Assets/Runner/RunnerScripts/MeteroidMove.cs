using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteroidMove : MonoBehaviour
{
    [SerializeField] float movementSpeed = -0.001f;
    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(this.movementSpeed, 0, 0);
        
    }
}
