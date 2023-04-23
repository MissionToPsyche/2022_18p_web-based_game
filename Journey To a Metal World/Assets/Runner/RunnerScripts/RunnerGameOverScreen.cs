using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

/**
    built initially using the game over screen from the memory game as a base
    for lack of better title it is called RunnerGameOverScreen but this is also 
    technically the game win screen. perhaps I can change the text depending on the situation?
*/
public class RunnerGameOverScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI points_text;
    [SerializeField] TextMeshProUGUI high_score_text;
    [SerializeField] TextMeshProUGUI game_outcome_text;

    public void SetupScreen(int score, int high_score, bool game_win)
    {
        gameObject.SetActive(true);
        this.points_text.text = "Your Score: \n" + score.ToString();
        this.high_score_text.text = "High Score: \n" + high_score.ToString();
        if (game_win == true)
        {
            this.game_outcome_text.text = "You Win!";
        }
        else
        {
            this.game_outcome_text.text = "Game Over!";
        }
    }
}
