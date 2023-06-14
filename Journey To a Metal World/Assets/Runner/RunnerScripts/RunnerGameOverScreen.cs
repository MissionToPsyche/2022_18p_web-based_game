using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

/**
    built initially using the game over screen from the memory game as a base
    for lack of better title it is called RunnerGameOverScreen but this is also 
    technically the game win screen. 
    Needs to be attached to the game over screen object in the game
*/
public class RunnerGameOverScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI points_text;
    [SerializeField] TextMeshProUGUI high_score_text;
    [SerializeField] TextMeshProUGUI game_outcome_text;
    // the text areas were given to the serialized fields so the file knows where to update the text

    /**
        The method that takes care of updating the text in the game over / game win screen
        Takes in an
        int score // current score
        int high score // the existing one so if you set a new high score this would be equal to the score
        bool game_win // tells whether or not the game was a win. false means not
    */
    public void SetupScreen(int score, int high_score, bool game_win)
    {
        gameObject.SetActive(true); // we need to set the game over/win screen to be visible

        // then we start collecting the text
        this.points_text.text = "Your Score: \n" + score.ToString();
        this.high_score_text.text = "High Score: \n" + high_score.ToString();

        //there is a slight difference in header depending on if the player won the game or not
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
