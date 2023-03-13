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

    void Start()
    {
        id = PartLocationsData.ids[name];
        rend = GetComponent<SpriteRenderer>();
    }

    /*
    * Function: OnTriggerEnter2D
    * --------------------
    * Part location turns opaque
    */
    void OnTriggerEnter2D(Collider2D collision)
    {
        rend.color = opaque;
    }

    /*
    * Function: OnTriggerExit2D
    * --------------------
    * Part location turns transparent
    */
    void OnTriggerExit2D(Collider2D other) 
    {
        rend.color = transparent;
    }

    public int getPartLocationID() 
    {
        return id;
    }
}
