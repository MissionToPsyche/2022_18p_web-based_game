/*
*   The MemoryGameController class is responsible for the higher level controls within the Memory game.
*   The main properties of the class are to enable and disable the objects on the screen, notify the
*   other scripts if a pattern has already been generated, and return the objects in an array for other scripts
*   to use.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MemoryGameController : MonoBehaviour
{
    [SerializeField] GameObject[] solar_components;
    private int difficulty_gauge = 1; // caps out at 20
    private int high_score = 0;

    private bool finish_pattern_once = false;


    void Awake()
    {
        if (PlayerPrefs.HasKey("memory_high_score"))
        {
            high_score = PlayerPrefs.GetInt("memory_high_score");
        }
        else
        {
            saveHighScore();
        }
    }


    /// <summary> At the start of the game, prevents players from clicking on any of the objects on the screen. </summary>
    void Start()
    {
        disableSolarComponentClick();
    }
    


    /// <summary> Returns the array holding all of the objects that can be found on the screen. </summary>
    /// <returns> A GameObject Array, with all the game objects currently in the game. </returns>
    public GameObject[] getSolarComponentArray()
    {
        return solar_components;
    }


    /// <summary> Returns the difficulty level of the current game which is useful for pattern generation or points. </summary>
    /// <returns> A 32-bit positive integer, representing the difficulty level. </returns>
    public int getDifficultyGauge()
    {
        return difficulty_gauge;
    }


    public int getHighScore()
    {
        return high_score;
    }


    public void setHighScore(int possible_high_score)
    {
        // we only update the high score if the new one is larger
        if (possible_high_score > high_score)
        {
            high_score = possible_high_score;
        }
    }


    public void saveHighScore()
    {
        PlayerPrefs.SetInt("memory_high_score", high_score);
    }


    /// <summary> Changes the boolean flag that lets the game know a pattern has been generated. </summary>
    /// <param name="flag"> A boolean that you want to change the current value to. </param>
    public void changePatternPermissions(bool flag)
    {
        finish_pattern_once = flag;
    }


    /// <summary> Get the boolean flag that lets us know if a pattern has been generated for the first time. </summary>
    /// <returns> A boolean. </returns>
    public bool getPatternPermission()
    {
        return finish_pattern_once;
    }

    
    /// <summary> Increment the difficulty of the game, typically by 1. </summary>
    /// <param name="num_to_increase"> A number that should be non-negative that will increase the difficulty of the game. </param>
    public void addToDifficultyGauge(int num_to_increase)
    {
        if (num_to_increase < 0)
        {
            return;
        }
        if (difficulty_gauge + num_to_increase > 20)
        {
            return;
        }
        
        difficulty_gauge += num_to_increase;
    }


    /// <summary> Disables all the buttons on the canvas, preventing players from clicking on any of the objects on screen. </summary>
    public void disableSolarComponentClick()
    {
        foreach(GameObject obj in solar_components)
        {
            // Debug.Log(obj.name + " has been disabled");
            obj.GetComponent<Button>().interactable = false;
        }
    }


    /// <summary> Enables all the buttons on the canvas, allow players to click on the objects on screen. </summary>
    public void enableSolarComponentClick()
    {
        foreach(GameObject obj in solar_components)
        {
            // Debug.Log(obj.name + " has been enabled");
            obj.GetComponent<Button>().interactable = true;
        }
    }
}
