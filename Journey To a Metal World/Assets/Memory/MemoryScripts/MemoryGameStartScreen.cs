using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryGameStartScreen : MonoBehaviour
{
    private bool game_started = false;
    [SerializeField] GameObject start_screen_overlay;


    public void startGame()
    {
        game_started = true;
        start_screen_overlay.SetActive(false);
    }


    public bool hasGameStarted()
    {
        return game_started;
    }
}
