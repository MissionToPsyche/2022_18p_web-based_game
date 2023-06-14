using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
    Attached to the Runner game Instructions screen
    Used to disappear the instructions and allow the orbiter
    to move after the ok button is pressed 
    
*/
public class RunnerStartScreen : MonoBehaviour
{
    bool game_started = false;
    [SerializeField] GameObject start_screen_overlay;
    RunnerEnvironmentMovementManager environment_manager;
    RunnerOrbital orbital_control;

    /**
        We need to obtain references to the RunnerEnvironmentMovementManager
        to be able to use its BeginGame method and the RunnerOrbital so that we
        can turn on its ability to move
    */
    void Start()
    {
        environment_manager = FindObjectOfType<RunnerEnvironmentMovementManager>();;
        orbital_control = FindObjectOfType<RunnerOrbital>();
    }

    /**
        Starts the game, disappears the instructions, allows player movement
    */
    public void StartGame()
    {
        this.game_started = true;
        start_screen_overlay.SetActive(false);

        this.environment_manager.BeginGame();
        this.orbital_control.AllowOrbitalMovement();
        // Debug.Log("Orbital movement enabled");
    }


    /**
        Getter for checking if the game started yet
    */
    public bool HasGameStarted()
    {
        return game_started;
        
    }
}
