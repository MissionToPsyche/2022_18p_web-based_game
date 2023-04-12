using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverDisplay : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI score_text;
    [SerializeField] public TextMeshProUGUI high_score_text;
    [SerializeField] public GameObject logicManager;


    public void setup(int score)
    {   
        score_text.text = "Your Score: \n" + score.ToString();
        // // high_score_text.text = "High Score: \n" + high_score.ToString();
        gameObject.SetActive(true);
    }
}
