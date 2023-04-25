using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerStartScreen : MonoBehaviour
{
    bool game_started = false;
    [SerializeField] GameObject start_screen_overlay;
    RunnerEnvironmentMovementManager environment_manager;
    RunnerOrbital orbital_control;

    void Start()
    {
        environment_manager = FindObjectOfType<RunnerEnvironmentMovementManager>();;
        orbital_control = FindObjectOfType<RunnerOrbital>();
    }
    public void StartGame()
    {
        this.game_started = true;
        start_screen_overlay.SetActive(false);

        this.environment_manager.BeginGame();
        this.orbital_control.AllowOrbitalMovement();
        Debug.Log("Orbital movement enabled");
    }


    public bool HasGameStarted()
    {
        return game_started;
        
    }
}
