using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MemoryGameOverScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI points_text;
    [SerializeField] TextMeshProUGUI high_score_text;

    public void setup(int score, int high_score)
    {
        gameObject.SetActive(true);
        points_text.text = "Your Score: \n" + score.ToString();
        high_score_text.text = "High Score: \n" + high_score.ToString();
    }


    public void restartButton()
    {
        SceneManager.LoadScene("Memory");
    }


    public void exitButton()
    {
        SceneManager.LoadScene("Main Menu");
    }
}