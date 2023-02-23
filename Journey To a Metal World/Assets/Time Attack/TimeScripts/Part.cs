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

    // Start is called before the first frame update
    void Start()
    {
        id = Random.Range(0, 5);
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<Logic>();

        loadSprite();
    }

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

    // Update is called once per frame
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
