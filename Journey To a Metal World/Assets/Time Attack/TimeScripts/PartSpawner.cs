using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartSpawner : MonoBehaviour
{
    [SerializeField] public GameObject part;
    [SerializeField] public float spawnRate = 1.5f;

    private float timer;

    void Start()
    {
        timer = 0;
        Instantiate(part, transform.position, transform.rotation);
    }

    /*
    * Function: Update
    * --------------------
    * Increases the timer if not enough time has elapsed, else spawns
    * a part
    */
    void Update()
    {
        if (timer < spawnRate) {
            timer += Time.deltaTime;
        } else {
            Instantiate(part, transform.position, transform.rotation);
            timer = 0;
        }
    }
}
