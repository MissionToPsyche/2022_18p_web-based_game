using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSwap : MonoBehaviour
{
    [SerializeField] public GameObject obj;
    [SerializeField] public Sprite defaultSprite;
    [SerializeField] public Sprite highlightedSprite;

    private SpriteRenderer rend;


    /// <summary> Grabs the objects that we will be changing (about and credits drawers) </summary>
    private void Awake() {
        rend = obj.GetComponent<SpriteRenderer>();
    }


    /// <summary> Resets the sprite back to the original </summary>
    public void unhighlighted()
    {
        rend.sprite = defaultSprite;
    }


    /// <summary> Updates the sprite to the new image </summary>
    public void highlighted()
    {
        rend.sprite = highlightedSprite;
    }


}
