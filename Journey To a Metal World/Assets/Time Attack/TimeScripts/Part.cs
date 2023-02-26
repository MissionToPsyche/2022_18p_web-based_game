using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Logic;

public class Part : MonoBehaviour
{
    [SerializeField] public Sprite antennaSprite;
    [SerializeField] public Sprite magnetometerSprite;
    [SerializeField] public Sprite spectrometerSprite;
    [SerializeField] public Sprite positiveYPanelSprite;
    [SerializeField] public Sprite negativeYPanelSprite;

    private static int END_X = -15;

    private int id;
    private bool isDragging;
    private bool isOverPartLocation;

    private Logic logic;

    void Start()
    {
        id = Random.Range(0, 5);
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<Logic>();

        loadSprite();
    }

    /*
    * Function: loadSprite
    * --------------------
    * Loads the correct sprite to be rendered for the part
    *
    * NOTES:    Uses part ID to assign sprite
    */
    private void loadSprite() 
    {
        Dictionary<int, Sprite> sprites = new Dictionary<int, Sprite>() {
            {0, antennaSprite},
            {1, magnetometerSprite},
            {2, spectrometerSprite},
            {3, positiveYPanelSprite},
            {4, negativeYPanelSprite}
        };

        GetComponent<SpriteRenderer>().sprite = sprites[id];
    }

    /*
    * Function: Update
    * --------------------
    * Destroys the part if necessary conditions are met, else updates 
    * the position of the part to the position of the mouse
    */
    void Update()
    {
        if (transform.position.x < END_X) 
        {
            Destroy(gameObject);
        }

        if (!isDragging && isOverPartLocation) {
            logic.incrementScore();
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

    /*
    * Function: OnMouseDown
    * --------------------
    * Part is being dragged
    */
    void OnMouseDown() 
    {
        isDragging = true;
    }

    /*
    * Function: OnMouseUp
    * --------------------
    * Part is not being dragged
    */
    void OnMouseUp() 
    {
        isDragging = false;
    }

    /*
    * Function: OnTriggerEnter2D
    * --------------------
    * Part triggers the part location with the matching ID
    */
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

    /*
    * Function: OnTriggerExit2D
    * --------------------
    * Part stops triggering the part location with the matching ID
    */
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
