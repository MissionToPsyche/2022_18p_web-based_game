using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part : MonoBehaviour
{
    private int id;
    private static int END_X = -10;

    // Start is called before the first frame update
    void Start()
    {
        id = Random.Range(0, 6);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < END_X) 
        {
            Destroy(gameObject);
        }
    }

    public int getPartID()
    {
        return id;
    }
}
