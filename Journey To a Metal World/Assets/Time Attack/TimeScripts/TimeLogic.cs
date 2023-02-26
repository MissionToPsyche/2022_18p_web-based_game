using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeLogic : MonoBehaviour
{
    [SerializeField] public Text timeText;

    private float timer;

    void Start() 
    {
        timer = 30;
    }

    void Update() 
    {
        if (timer > 0) {
            timer -= Time.deltaTime;
            updateTime();
        }
    }

    void updateTime()
    {
        timeText.text = "Time Left: " + ((int) timer).ToString();
    }
}
