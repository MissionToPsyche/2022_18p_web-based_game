using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerStartScreen : MonoBehaviour
{
    bool game_started = false;
    [SerializeField] GameObject start_screen_overlay;


    public void StartGame()
    {
        this.game_started = true;
        start_screen_overlay.SetActive(false);
    }


    public bool HasGameStarted()
    {
        return game_started;
    }
}
