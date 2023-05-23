using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class HighestScore : MonoBehaviour
{
    public TMP_Text TextMeshPro;
    public static int highest_score = 0;

    void Update()
    {
        TextMeshPro.text = "High Score:\n" + Score.high_score.ToString();
    }
}
