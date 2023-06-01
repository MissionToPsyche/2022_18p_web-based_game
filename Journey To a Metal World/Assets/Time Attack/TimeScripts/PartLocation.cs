/*
*   The PartLocation class is responsible for the logic of part locations in the Time Attack game.
*   This class controls the highlighting of a part location when the user moves a part over it.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartLocation : MonoBehaviour
{
    private static Color TRANSPARENT = new Color(1, 1, 1, 0.75f);
    private static Color OPAQUE = new Color(1, 1, 1, 1);
    private static Dictionary<string, int> PART_LOCATIONS = new Dictionary<string, int>() {
        {"Antenna Part Location", 0},
        {"Magnetometer Part Location", 1},
        {"Spectrometer Part Location", 2},
        {"+Y Part Location", 3},
        {"-Y Part Location", 4}
    };


    private int partLocationType;
    private SpriteRenderer rend;


    void Start()
    {
        partLocationType = PART_LOCATIONS[name];
        rend = GetComponent<SpriteRenderer>();
    }


    /*
    * Function: OnTriggerEnter2D
    * --------------------
    * Part location turns opaque
    */
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Part" && collision.gameObject.GetComponent<PartLogic>().getIsDragging()) {
            rend.color = OPAQUE;
        }
    }


    /*
    * Function: OnTriggerExit2D
    * --------------------
    * Part location turns transparent
    */
    void OnTriggerExit2D(Collider2D other) 
    {
        rend.color = TRANSPARENT;
    }


    public int getPartLocationType() 
    {
        return partLocationType;
    }
}
