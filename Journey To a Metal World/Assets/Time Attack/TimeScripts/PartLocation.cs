using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PartLocationsData;

public class PartLocation : MonoBehaviour
{
    private Color start_color;
    private Color triggered_color;
    private int id;
    private SpriteRenderer rend;

    // Start is called before the first frame update
    void Start()
    {
        start_color = PartLocationsData.start_colors[name];
        triggered_color = PartLocationsData.triggered_colors[name];
        id = PartLocationsData.ids[name];

        rend = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        rend.color = triggered_color;
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        rend.color = start_color;
    }

    public int getPartLocationID() 
    {
        return id;
    }
}
