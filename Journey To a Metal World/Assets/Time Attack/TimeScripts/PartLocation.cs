using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PartLocationsData;

public class PartLocation : MonoBehaviour
{
    private static Color transparent = new Color(1, 1, 1, 0.35f);
    private static Color opaque = new Color(1, 1, 1, 1);
    private int id;
    private SpriteRenderer rend;

    // Start is called before the first frame update
    void Start()
    {
        id = PartLocationsData.ids[name];
        rend = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        rend.color = opaque;
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        rend.color = transparent;
    }

    public int getPartLocationID() 
    {
        return id;
    }
}
