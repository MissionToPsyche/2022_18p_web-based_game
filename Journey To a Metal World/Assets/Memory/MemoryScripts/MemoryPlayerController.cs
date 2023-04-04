using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class MemoryPlayerController : MonoBehaviour
{
    [SerializeField] GameObject score_text;
    private MemoryPatternGenerator pattern_generator;
    private MemoryGameController controller;
    private Queue<GameObject> player_selection = new Queue<GameObject>();
    private int player_score = 0;
    private bool complete_pattern = false;
    private bool generated_pattern = false;
    

    private void Awake()
    {
        pattern_generator = FindObjectOfType<MemoryPatternGenerator>();
        controller = FindObjectOfType<MemoryGameController>();
    }

    
    private void Update()
    {
        // we also want to give a bit of a pause to give the player time before starting the next pattern
        if (!generated_pattern)
        {
            controller.disableSolarComponentClick();
            StartCoroutine(generatePatternWithPause());
        }
        roundController();
        
        // connects the player_score with the text that is shown on the screen
        score_text.GetComponent<TextMeshProUGUI>().text = "Score: " + player_score.ToString();
    }


    public Queue<GameObject> getPlayerSelection()
    {
        return player_selection;
    }


    IEnumerator generatePatternWithPause()
    {
        yield return new WaitForSeconds(1.5f);
        if (!generated_pattern)
        {
            // if the pattern has not been generated, then generate it
            pattern_generator.generatePattern();
            generated_pattern = true;
        }
    }


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
        }

        // setup timer

        // setup if selection was correct
        if (player_selection.Count != 0)
        {
            if (pattern_generator.getPatternStorage().Peek() != player_selection.Peek())
            {
                Debug.Log("Invalid Selection");
                player_selection.Clear();
                // temporary will change later on
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
        }
    }

}
