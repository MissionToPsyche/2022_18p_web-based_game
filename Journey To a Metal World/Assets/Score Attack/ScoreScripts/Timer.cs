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
        total_time = 60;
    }

    void Update()
    {
        if(total_time > 0){
            total_time -= Time.deltaTime;
        }
        else{
            
            total_time += 60;
        }
        
        TextMeshPro.text = "Time: " + ((int)total_time).ToString();
    }

}
