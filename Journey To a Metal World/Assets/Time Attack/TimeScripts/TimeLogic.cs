/*
*   The TimeLogic class manages the time for the Time Attack game.
*   This class determines whether time has run out and the game is over.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TimeLogic : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI timeText;
    [SerializeField] public float totalTime;
    [SerializeField] public GameObject gameOverDisplay;

    private float timer;
    private bool isGameOver;
    private ScoreLogic scoreLogic;


    void Start() 
    {
        timer = totalTime;
        isGameOver = false;
        scoreLogic = GetComponent<ScoreLogic>();
    }


    void Update() 
    {
        if (!isGameOver) {
            if (timer > 0) {
                timer -= Time.deltaTime;
                updateTime();
            } else {
                scoreLogic.setHighScore();
                scoreLogic.saveHighScore();
                gameOverDisplay.GetComponent<GameOverDisplay>().setup(scoreLogic.getScore(), scoreLogic.getHighScore());
                isGameOver = true;
            }
        }
        
    }


    void updateTime()
    {
        timeText.text = "Time: " + ((int) timer).ToString();
    }


    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void exitGame()
    {
        SceneManager.LoadScene("Main Menu");
    }


    public bool getIsGameOver()
    {
        return isGameOver;
    }
}
