using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public static int total_score = 0;
    public TMP_Text TextMeshPro;
    public static int high_score = 0;
    
    void Start()
    {
        total_score = 0;
        high_score = 0;
        if (PlayerPrefs.HasKey("score_attack_high_score"))
        {
            high_score = PlayerPrefs.GetInt("score_attack_high_score");
        }
        else
        {
            saveHighScore();
        }
    }

    void Update()
    {
        TextMeshPro.text = "Score:" + total_score.ToString();
        setHighScore();
    }

    public void setHighScore()
    {
        if(total_score > high_score)
        {
            high_score = total_score;
            saveHighScore();
        }
    }

    public void saveHighScore()
    {
        PlayerPrefs.SetInt("score_attack_high_score", high_score);
    }

}
