using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSwap : MonoBehaviour
{
    [SerializeField] public GameObject obj;
    [SerializeField] public Sprite defaultSprite;
    [SerializeField] public Sprite highlightedSprite;

    private SpriteRenderer rend;

    private void Awake() {
        rend = obj.GetComponent<SpriteRenderer>();
    }

    public void unhighlighted()
    {
        rend.sprite = defaultSprite;
    }


    public void highlighted()
    {
        rend.sprite = highlightedSprite;
    }


}
