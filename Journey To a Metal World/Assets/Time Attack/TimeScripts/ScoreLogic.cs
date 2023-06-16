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
    [SerializeField] public AudioSource pointSoundEffect;

    private int score;
    private int highScore;


    private void Awake() 
    {
        if (PlayerPrefs.HasKey("time_attack_high_score"))
        {
            highScore = PlayerPrefs.GetInt("time_attack_high_score");
        }
        else
        {
            saveHighScore();
        }
    }


    [ContextMenu("Increment Score")]
    public void incrementScore() 
    {
        score += 150;
        scoreText.text = "Score: " + score.ToString();
        pointSoundEffect.Play();
    }


    public int getScore()
    {
        return score;
    }


    public int getHighScore()
    {
        return highScore;
    }


    public void setHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
        }
    }


    public void saveHighScore()
    {
        PlayerPrefs.SetInt("time_attack_high_score", highScore);
    }
}
