using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunnerScore : MonoBehaviour
{

    [SerializeField] public Text score_text;

    int score = 0;
    
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
        score = Mathf.Clamp(this.score, 0, int.MaxValue); // keeps score from going below 0
        Debug.Log("New Score: " + this.score);
    }
    

    public void ResetScore()
    {
        this.score = 0;
    }
}
