using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MemoryGameOverScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI points_text;
    [SerializeField] TextMeshProUGUI high_score_text;


    /// <summary> Enables the game over screen and displays a player's high score and current score. </summary>
    public void setup(int score, int high_score)
    {
        gameObject.SetActive(true);
        points_text.text = "Your Score: \n" + score.ToString();
        high_score_text.text = "High Score: \n" + high_score.ToString();
    }


    /// <summary> Restarts the game when the restart button is pressed. </summary>
    public void restartButton()
    {
        SceneManager.LoadScene("Memory");
    }


    /// <summary> Returns the player to the main menu when the button is pressed. </summary>
    public void exitButton()
    {
        SceneManager.LoadScene("Main Menu");
    }
}