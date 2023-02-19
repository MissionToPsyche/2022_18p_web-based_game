using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartSpawner : MonoBehaviour
{
    public GameObject part;
    public float spawnRate = 3;
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(part, transform.position, transform.rotation);
    }

    // Update is called once per frame
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
