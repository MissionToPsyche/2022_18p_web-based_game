using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverDisplay : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI scoreText;
    [SerializeField] public TextMeshProUGUI highScoreText;
    [SerializeField] public GameObject logicManager;


    public void setup(int score, int highScore)
    {   
        scoreText.text = "Your Score: \n" + score.ToString();
        highScoreText.text = "High Score: \n" + highScore.ToString();
        gameObject.SetActive(true);
    }
}
