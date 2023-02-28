using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MemoryGameController : MonoBehaviour
{
    [SerializeField] GameObject[] solar_components;
    private int difficultyGauge = 0; // caps out at 15 or maybe 20

    void Start()
    {
        disableSolarComponentClick();
    }


    private int generateRandomSolarComponentIndex()
    {
        return Random.Range(0, solar_components.Length);
    }


    private void disableSolarComponentClick()
    {
        foreach(GameObject obj in solar_components)
        {
            Debug.Log(obj.name + " has been disabled");
            obj.GetComponent<Button>().interactable = false;
        }
    }


    private void enableSolarComponentClick()
    {
        foreach(GameObject obj in solar_components)
        {
            Debug.Log(obj.name + " has been enabled");
            obj.GetComponent<Button>().interactable = true;
        }
    }


    private void displayMemoryPattern()
    {

    }
}
