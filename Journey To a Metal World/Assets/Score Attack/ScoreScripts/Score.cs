using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public static int total_score = 0;
    public TMP_Text TextMeshPro;
    void Start()
    {
        total_score = 0;
    }

    void Update()
    {
        TextMeshPro.text = "Score:" + total_score.ToString();
    }

}
