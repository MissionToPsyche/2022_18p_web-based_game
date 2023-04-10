using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class HighestScore : MonoBehaviour
{
    public TMP_Text TextMeshPro;
    public static int highest_score = 0;
    // Update is called once per frame
    void Update()
    {
        if(Score.total_score > highest_score){
            highest_score = Score.total_score;
        }
        TextMeshPro.text = "High Score:\n" + highest_score.ToString();
    }
}
