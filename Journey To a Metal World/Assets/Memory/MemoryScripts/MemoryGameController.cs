using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MemoryGameController : MonoBehaviour
{
    [SerializeField] GameObject[] solar_components;
    private int difficultyGauge = 3; // caps out at 15 or maybe 20

    void Start()
    {
        //disableSolarComponentClick();
    }


    public GameObject[] getSolarComponentArray()
    {
        return solar_components;
    }


    public int getDifficultyGauge()
    {
        return difficultyGauge;
    }


    public void addToDifficultyGauge(int num_to_increase)
    {
        if (num_to_increase < 0)
        {
            return;
        }
        if (difficultyGauge + num_to_increase > 20)
        {
            return;
        }
        
        difficultyGauge += num_to_increase;
    }


    public void disableSolarComponentClick()
    {
        foreach(GameObject obj in solar_components)
        {
            Debug.Log(obj.name + " has been disabled");
            obj.GetComponent<Button>().interactable = false;
        }
    }


    public void enableSolarComponentClick()
    {
        foreach(GameObject obj in solar_components)
        {
            Debug.Log(obj.name + " has been enabled");
            obj.GetComponent<Button>().interactable = true;
        }
    }
}
