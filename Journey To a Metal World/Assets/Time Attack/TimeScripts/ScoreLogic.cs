/*
*   The ScoreLogic class manages the score of the Time Attack game.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreLogic : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI scoreText;

    private int score;


    [ContextMenu("Increment Score")]
    public void incrementScore() 
    {
        score += 1;
        scoreText.text = "Score: " + score.ToString();
    }
}
