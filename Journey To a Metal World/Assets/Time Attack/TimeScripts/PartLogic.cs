/*
*   The PartLogic class is responsible for the logic of the parts within the Time Attack game.
*   This class manages the click-and-drag mechanic of parts and increments the player's total points
*   when a part is successfully moved to its correct location.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartLogic : MonoBehaviour
{
    private static int END_X = -15;
    private static float RIGHT_BORDER = 12.5f;
    private static float LEFT_BORDER = -12.5f;
    private static float TOP_BORDER = 7f;
    private static float BOTTOM_BORDER = -5f;
    private static ScoreLogic scoreLogic;
    private static TimeLogic timeLogic;

    private int partType;
    private bool isDragging;
    private bool isOverPartLocation;

    [SerializeField] public Sprite antennaSprite;
    [SerializeField] public Sprite magnetometerSprite;
    [SerializeField] public Sprite spectrometerSprite;
    [SerializeField] public Sprite positiveYPanelSprite;
    [SerializeField] public Sprite negativeYPanelSprite;

    public static Dictionary<int, Sprite> PART_SPRITES;


    void Awake() 
    {
        scoreLogic = GameObject.FindGameObjectWithTag("Logic").GetComponent<ScoreLogic>();
        timeLogic = GameObject.FindGameObjectWithTag("Logic").GetComponent<TimeLogic>();

        PART_SPRITES = new Dictionary<int, Sprite>() {
            {0, antennaSprite},
            {1, magnetometerSprite},
            {2, spectrometerSprite},
            {3, positiveYPanelSprite},
            {4, negativeYPanelSprite}
        };
    }


    void Start()
    {
        partType = generatePartType();
        loadSprite();
    }


    /// <summary> Destroys the part if necessary conditions are met, else updates the position 
    /// of the part to the position of the mouse </summary>
    void Update()
    {
        if (transform.position.x < END_X) 
        {
            Destroy(gameObject);
        }

        if (!timeLogic.getIsGameOver() && isDragging) {
            movePart();
        }
    }


    /// <summary> Randomly generates a part type to be assigned to the part </summary>
    /// <returns> An integer representing the type of the part </returns>
    private int generatePartType()
    {
        return Random.Range(0, 5);
    }


    private void loadSprite()
    {

        GetComponent<SpriteRenderer>().sprite = PART_SPRITES[partType];
    }


    /// <summary> The player is moving the part </summary>
    void OnMouseDown() 
    {
        isDragging = true;
    }


    /// <summary> The player is releasing the part </summary>
    void OnMouseUp() 
    {
        isDragging = false;

        if (isOverPartLocation) {
            scoreLogic.incrementScore();
            Destroy(gameObject);
        }
    }


    /// <summary> Determines if the part being moved is over the correct part location </summary>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Part Location")
        {
            if (other.GetComponent<PartLocation>().getPartLocationType() == partType) 
            {
                isOverPartLocation = true;
            }
        }
    }


    /// <summary> Determines if the part being moved has left the correct part location </summary>
    void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Part Location")
        {
            if (other.GetComponent<PartLocation>().getPartLocationType() == partType) 
            {
                isOverPartLocation = false;
            }
        }
    }

    public void movePart() {
        Vector2 mousePosition = Input.mousePosition;
        Vector2 mousePositionRelative = Camera.main.ScreenToWorldPoint(mousePosition);

        if (mousePositionRelative.x > RIGHT_BORDER) {
            mousePositionRelative.x = RIGHT_BORDER;
        } else if (mousePositionRelative.x < LEFT_BORDER) {
            mousePositionRelative.x = LEFT_BORDER;
        }

        if (mousePositionRelative.y > TOP_BORDER) {
            mousePositionRelative.y = TOP_BORDER;
        } else if (mousePositionRelative.y < BOTTOM_BORDER) {
            mousePositionRelative.y = BOTTOM_BORDER;
        }

        float newPositionX = mousePositionRelative.x - transform.position.x;
        float newPositionY = mousePositionRelative.y - transform.position.y;

        transform.Translate(newPositionX, newPositionY, 0);
    }
}
