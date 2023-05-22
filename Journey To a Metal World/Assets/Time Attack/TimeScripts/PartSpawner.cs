/*
*   The PartSpawner class handles the creation of new parts for the Time Attack game.
*   This class spawns parts at a regular interval determined by the value assigned
*   to spawnRate.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartSpawner : MonoBehaviour
{
    [SerializeField] public GameObject part;
    [SerializeField] public float spawnRate = 1.5f;

    private TimeLogic timeLogic;
    private float timer;


    void Start()
    {
        timer = spawnRate;
        timeLogic = GameObject.FindGameObjectWithTag("Logic").GetComponent<TimeLogic>();
    }


    /// <summary> Increments the timer until a new part should be created </summary>
    void Update()
    {
        if (timeLogic.getIsGameStart() && !timeLogic.getIsGameOver()) {
            if (timer < spawnRate) {
                timer += Time.deltaTime;
            } else {
                timer = 0;
                SpawnPart();
            }
        }
    }


    private void SpawnPart()
    {
        Instantiate(part, transform.position, transform.rotation);
    }
}
