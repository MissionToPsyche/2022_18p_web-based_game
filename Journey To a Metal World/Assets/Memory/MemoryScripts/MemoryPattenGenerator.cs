using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MemoryPattenGenerator : MonoBehaviour
{
    private MemoryGameController controller;


    private void Awake()
    {
        controller = FindObjectOfType<MemoryGameController>();
    }


    private void Update()
    {
        // temporary to show that it works
        displayMemoryPattern();
    }


    private int generateRandomSolarComponentIndex()
    {
        return Random.Range(0, controller.getSolarComponentArray().Length);
    }


    private void displayMemoryPattern()
    {
        if (controller.getDifficultyGauge() == 5)
        {
            return;
        }
        for (int difficulty_index = 0; difficulty_index <= controller.getDifficultyGauge(); ++difficulty_index)
        {
            Debug.Log("object: " + controller.getSolarComponentArray()[generateRandomSolarComponentIndex()]);
        }
        Debug.Log("\niteration: " + controller.getDifficultyGauge());
        controller.addToDifficultyGauge(1);
        
    }
}
