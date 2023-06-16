using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryGameStartScreen : MonoBehaviour
{
    private bool game_started = false;
    [SerializeField] GameObject start_screen_overlay;
    [SerializeField] ParticleSystem stars;


    /// <summary> When a player enters the game from the main menu, they are met with this start screen that 
    /// shows them how to play the game. It can also be triggered when the game is reststarted. </summary> 
    public void startGame()
    {
        game_started = true;
        start_screen_overlay.SetActive(false);
        stars.gameObject.SetActive(true);
        stars.Play();
    }

    /// <summary> Provides a boolean that indicates that the game has started. </summary>
    /// <returns> A boolean. </returns>
    public bool hasGameStarted()
    {
        return game_started;
    }
}
