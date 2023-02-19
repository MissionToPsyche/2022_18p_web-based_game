using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part : MonoBehaviour
{
    private int id;
    private static int END_X = -10;
    private bool isDragging;

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

    int getPartID()
    {
        return id;
    }
}
