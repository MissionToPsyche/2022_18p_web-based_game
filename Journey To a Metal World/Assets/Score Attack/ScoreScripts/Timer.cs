using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public static float total_time = 0;
    public TMP_Text TextMeshPro;
    void Start()
    {
        total_time = 10;
    }

    void Update()
    {
        if(EndScene.timeout == false && total_time > 0){
            total_time -= Time.deltaTime;
        }
        else{
            total_time = 0;
        }
        
        TextMeshPro.text = "Time: " + ((int)total_time).ToString();
    }

}
