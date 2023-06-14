using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
    Contains the methods for managing the score of the Runner game
*/
public class RunnerScore : MonoBehaviour
{
   
    int current_high_score = 0;
    int score = 0;
    
    /** 
        the first thing done is check if there is an existing high score for the runner
        game saved. if there is we set our current high score value to it. Otherwise
        we initialize it to 0 in storage;
    */
    void Awake() 
    {
        if (PlayerPrefs.HasKey("Runner_high_score"))
        {
            this.current_high_score = PlayerPrefs.GetInt("Runner_high_score");
        }
        else
        {
            SetRunnerHighScore(this.score);    
        }
    }

    /**
        Getter for checking the score
    */
    public int GetScore()
    {
        return this.score;
    }

    /**
        Adds integer @val to the existing score
        Note that this function does keep score from becoming a negative value too
    */
    public void AddToScore(int val) 
    {
        this.score += val;
        score = Mathf.Clamp(this.score, 0, int.MaxValue); // keeps score from going below 0 or higher than
        // the maximum int value
        // Debug.Log("New Score: " + this.score);
    }
    

    /**
        Resets the score value to zero
    */
    public void ResetScore()
    {
        this.score = 0;
    }

    /**
        sets the high score for the Runner game to be the 
        @new_score parameter
        if we have extra time we can do maybe an effect when we get a new 
        high score or reach a high 
    */
    public void SetRunnerHighScore(int new_score)
    {
        PlayerPrefs.SetInt("Runner_high_score", new_score);
        InformationKeeper.SetRunnerHighScore(new_score);
    }

    /**
        Getter method for the currently stored high score for the Runner game
    */
    public int GetRunnerHighScore()
    {
        return PlayerPrefs.GetInt("Runner_high_score");
        // InformationKeeper.GetRunnerHighScore();
    }

    /**
        Checks if the int @new_score parameter is larger than the current high score
        returns true if it is. Else returns false
    */
    public bool IsLargerThanCurrentHighScore(int new_score)
    {
        if (new_score > this.current_high_score)
        {
            return true;
        }
        return false;
    }   

    /**
        Checks if the int @new_score paramter is tied with the current high score 
        this method is currently unused but I was theorizing having a special additional 
        sparkle effect or something for any case where the player had a new high score or 
        tied their existing high score
    */
    public bool IsTiedWithCurrentHighScore(int new_score)
    {
        if (new_score == this.current_high_score)
        {
            return true;
        }
        return false;

    }

    /**
        updates the currently stored high score value in PlayerPrefs
        Returns 2 if it is a new high score (higher than your previous high score)
        return 1 if it ties with the current high score
        return 0 if it is less than the current high score
    */
    public int UpdateHighScore(int new_score)
    {
        if (new_score > this.current_high_score)
        {
            // new high score. need to set it.
            SetRunnerHighScore(new_score);
            return 2;
        }
        else if (new_score == this.current_high_score)
        {
            //tied high score. could use a sound effect and extra text field
            return 1;
        }
        else
        {
            //did not tie or exceed high score. 
            return 0;
        }
    }

}
