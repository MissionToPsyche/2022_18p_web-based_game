using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PartsData;

public class Part : MonoBehaviour
{
    private int id;
    private static int END_X = -10;
    private bool isDragging;
    private bool isOverPartLocation;

    // Start is called before the first frame update
    void Start()
    {
        id = Random.Range(0, 5);
        GetComponent<SpriteRenderer>().color = PartsData.start_colors[id];
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < END_X) 
        {
            Destroy(gameObject);
        }

        if (!isDragging && isOverPartLocation) {
            Destroy(gameObject);
        }

        if (isDragging) {
            Vector2 mousePosition = Input.mousePosition;
            Vector2 mousePositionRelative = Camera.main.ScreenToWorldPoint(mousePosition);

            float newPositionX = mousePositionRelative.x - transform.position.x;
            float newPositionY = mousePositionRelative.y - transform.position.y;

            transform.Translate(newPositionX, newPositionY, 0);
        }
    }

    void OnMouseDown() 
    {
        isDragging = true;
    }

    void OnMouseUp() 
    {
        isDragging = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Part Location")
        {
            if (other.GetComponent<PartLocation>().getPartLocationID() == id) 
            {
                isOverPartLocation = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Part Location")
        {
            if (other.GetComponent<PartLocation>().getPartLocationID() == id) 
            {
                isOverPartLocation = false;
            }
        }
    }
}
