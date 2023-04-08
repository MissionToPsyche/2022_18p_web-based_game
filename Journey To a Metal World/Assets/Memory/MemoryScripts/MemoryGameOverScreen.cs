using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MemoryGameOverScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI points_text;

    public void setup(int score)
    {
        gameObject.SetActive(true);
        points_text.text = "High Score: \n" + score.ToString();
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
