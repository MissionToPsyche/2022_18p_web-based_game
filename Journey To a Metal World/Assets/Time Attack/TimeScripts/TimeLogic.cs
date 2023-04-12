/*
*   The TimeLogic class manages the time for the Time Attack game.
*   This class determines whether time has run out and the game is over.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeLogic : MonoBehaviour
{
    [SerializeField] public Text timeText;
    [SerializeField] public GameObject gameOver;

    private float timer;
    private bool isGameOver;


    void Start() 
    {
        timer = 30;
        isGameOver = false;
    }


    void Update() 
    {
        if (!isGameOver) {
            if (timer > 0) {
                timer -= Time.deltaTime;
                updateTime();
            } else {
                gameOver.SetActive(true);
                isGameOver = true;
            }
        }
        
    }


    void updateTime()
    {
        timeText.text = "Time Left: " + ((int) timer).ToString();
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
