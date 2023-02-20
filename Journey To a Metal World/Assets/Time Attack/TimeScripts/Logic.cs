using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Logic : MonoBehaviour
{
    private int score;
    [SerializeField] public Text scoreText;

    [ContextMenu("Increment Score")]
    public void incrementScore() 
    {
        score += 1;
        scoreText.text = score.ToString();
    }
}
