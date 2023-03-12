using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MemoryPlayerController : MonoBehaviour
{
    private MemoryPattenGenerator pattern_generator;
    private MemoryGameController controller;
    private ArrayList player_selection = new ArrayList();


    private void Awake()
    {
        pattern_generator = FindObjectOfType<MemoryPattenGenerator>();
        controller = FindObjectOfType<MemoryGameController>();
    }

    public void makeSelection()
    {

        // enables the player to click on the buttons
        controller.enableSolarComponentClick();

        // holds the number of solar objects that were randomly generated
        int patten_length = pattern_generator.getPatternStorage().Count;

        // setup timer

        // setup if selection was correct
    }

}
