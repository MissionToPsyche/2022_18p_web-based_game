/*
*   The MemoryPlayerController class holds our main game logic. It will check if the player makes the right selection and award them
*   with the points for the round.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MemoryPlayerController : MonoBehaviour
{
    [SerializeField] GameObject score_text;
    [SerializeField] TextMeshProUGUI time_text;
    [SerializeField] MemoryGameOverScreen game_over_screen;
    [SerializeField] MemoryGameStartScreen game_start_screen;
    private MemoryPatternGenerator pattern_generator;
    private MemoryGameController controller;
    private Queue<GameObject> player_selection = new Queue<GameObject>();
    private int player_score = 0;
    private bool complete_pattern = false;
    private bool generated_pattern = false;
    private bool time_on = false;
    private float time = 80f;
    

    /// <summary> Finds the MemoryPatternGenerator and MemoryGameController objects to use later on. </summary>
    private void Awake()
    {
        pattern_generator = FindObjectOfType<MemoryPatternGenerator>();
        controller = FindObjectOfType<MemoryGameController>();
        game_start_screen = FindObjectOfType<MemoryGameStartScreen>();
    }


    private void Start()
    {
        time_on = true;
    }

    
    /// <summary> Every frame, we will check to see if a new pattern needs to be generated. If it does, then we generate
    /// the pattern and prevent the player from clicking on the objects to prevent problems. We will also run our game logic and
    /// update our score. </summary>
    private void Update()
    {
        // we also want to give a bit of a pause to give the player time before starting the next pattern
        if (game_start_screen.hasGameStarted())
        {
            if (!generated_pattern)
            {
                controller.disableSolarComponentClick();
                StartCoroutine(generatePatternWithPause());
                time_on = false;
            }
            roundController();
            
            // connects the player_score with the text that is shown on the screen
            score_text.GetComponent<TextMeshProUGUI>().text = "Score: " + player_score.ToString();
        }
    }


    /// <summary> Gets the queue of all the objects the player has clicked on. </summary>
    /// <returns> A queue holding GameObjects. </returns>
    public Queue<GameObject> getPlayerSelection()
    {
        return player_selection;
    }


    /// <summary> Gets the score of the player that will be useful for displaying the score in the main screen. </summary>
    /// <returns An integer holding the score of the player. </returns>
    public int getScore()
    {
        return player_score;
    }


    /// <summary> Checks to see if we need to generate a new pattern </summary>
    /// <returns> An IEnumerator that pauses so that pattern generation is not instant and players have time to prepare. </returns>
    private IEnumerator generatePatternWithPause()
    {
        yield return new WaitForSeconds(1.5f);
        if (!generated_pattern)
        {
            // if the pattern has not been generated, then generate it
            pattern_generator.generatePattern();
            generated_pattern = true;
        }
    }


    public void showGameOver()
    {
        player_selection.Clear();
                
        // once you set a possible high score, you must also save it so that it won't be wiped on the restart
        controller.setHighScore(player_score);
        controller.saveHighScore();
                
        // for now it restarts the game whenever the player makees an invalid selection
        game_over_screen.setup(player_score, controller.getHighScore());
    }


    /// <summary> Our main game logic that checks whether or not the player correctly selects the GameObject that was generated.
    /// It will award players with points when the selection is correct, and trigger a game over when the selection is incorrect. </summary> 
    public void roundController()
    {
        // if the pattern has not been generated then do nothing
        if (!generated_pattern)
        {
            return;
        }

        if (controller.getPatternPermission())
        {
            controller.enableSolarComponentClick();
            controller.changePatternPermissions(false);
            time_on = true;
        }

        if (time_on)
        {
            if (time > 0)
            {
                time -= Time.deltaTime;
                int time_str = (int)time;
                time_text.text = "Time: " + time_str;
                // the time starts while the pattern is being generated fix this
            }
            else
            {
                showGameOver();
                time = 0;
                time_on = false;
                return;
            }
        }

        
        if (player_selection.Count != 0)
        {
            if (pattern_generator.getPatternStorage().Peek() != player_selection.Peek())
            {
                Debug.Log("Invalid Selection");
                time_on = false;
                showGameOver();

                return;
            }
            else
            {
                Debug.Log("Correct!");

                // if the pattern is complete then you add one to the score
                player_score += 10 * controller.getDifficultyGauge();

                pattern_generator.getPatternStorage().Dequeue();
                player_selection.Dequeue();
                    
                if (pattern_generator.getPatternStorage().Count == 0)
                {
                    // when we successfully repeat the pattern then it is complete and we can increase the player's score
                    complete_pattern = true;
                    controller.disableSolarComponentClick();
                }

            }
        }

        if (complete_pattern)
        {       
            // if we complete the pattern then we don't enter this if statement anymore (score doesn't go up)
            complete_pattern = false;
            // if we complete the pattern then we need to generate a new pattern (that is harder)
            generated_pattern = false;

            // if the player successfully repeats the pattern then we increase the difficulty
            Debug.Log("\niteration: " + controller.getDifficultyGauge());
            controller.addToDifficultyGauge(1);

            // for now we will lower the time as the round increases
            // they can get time back if they repeat the pattern
            time += controller.getDifficultyGauge() / 2;
            if (time > 80)
            {
                time = 80f;
            }
            time = (int)time;
            time_text.text = "Time: " + time.ToString();
        }
    }

}
