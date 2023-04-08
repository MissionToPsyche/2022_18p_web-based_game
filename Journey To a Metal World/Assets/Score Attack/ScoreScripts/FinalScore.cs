using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class FinalScore : MonoBehaviour
{
    public TMP_Text TextMeshPro;
    
    void Start()
    {

    }

    void Update()
    {
        TextMeshPro.text = "Your Score:\n" + Score.total_score.ToString();
    }
}
