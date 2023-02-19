using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartLocation : MonoBehaviour
{
    private Color START_COLOR = Color.white;
    private Color TRIGGERED_COLOR = Color.blue;
    private int id;
    private SpriteRenderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        renderer.color = TRIGGERED_COLOR;
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        renderer.color = START_COLOR;
    }
}
