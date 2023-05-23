using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunnerScore : MonoBehaviour
{

    // [SerializeField] public Text score_text;
    // combo it. make it the more you pass straight the more points you get each time
    
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetScore()
    {
        return this.score;
    }

    public void AddToScore(int val) 
    {
        this.score += val;
        score = Mathf.Clamp(this.score, 0, int.MaxValue); // keeps score from going below 0 or higher than
        // the maximum int value
        // Debug.Log("New Score: " + this.score);
    }
    

    public void ResetScore()
    {
        this.score = 0;
    }

    /**
        sets the high score for the Runner game to be the 
        @highScore parameter
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

    public bool IsLargerThanCurrentHighScore(int new_score)
    {
        if (new_score > this.current_high_score)
        {
            return true;
        }
        return false;
    }   

    public bool IsTiedWithCurrentHighScore(int new_score)
    {
        if (new_score == this.current_high_score)
        {
            return true;
        }
        return false;

    }

    /**
        updates the currently stored high score value in PlayerPrefs and set locally
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

    // public void 

}
