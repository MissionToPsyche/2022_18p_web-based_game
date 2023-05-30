using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

using UnityEngine.UI;
using TMPro;

public class RunnerUI : MonoBehaviour
{
    string MAIN_MENU_SCENE_NAME = "Main Menu";
    // [Header("Lives")]
    // [SerializeField] Slider lives_slider;
    // [SerializeField] RunnerOrbital player_lives;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI score_text;
    [SerializeField] TextMeshProUGUI combo_text;
    RunnerScore score_keeper;
    RunnerAudioPlayer audio_player;
    bool ready_to_play_combo_sound = false;

    void Awake() 
    {
        this.score_keeper = FindObjectOfType<RunnerScore>();   
        this.audio_player = FindObjectOfType<RunnerAudioPlayer>();
    }

    /**
    SetComboToZero is a static method that makes sure combos cannot be carried over restarts 
    this edge case is due to the fact that combo was created from a static variable 
    */
     void Start() 
     {
        RunnerMeteoroidPoints.SetComboToZero();
        // player_lives.maxValue = player_lives.GetLives();    
    }

    void Update()
    {
        this.score_text.text = this.score_keeper.GetScore().ToString();
        string old_combo_text = this.combo_text.text;
        this.combo_text.text = RunnerMeteoroidPoints.GetCombo().ToString();
        // due to the fact that I don't want this to have to be checked in every meteoroid (and to avoid them from triggering
        // multiple times for one combo up, it needs to go in a script that only appears once. 
        // additionally, this script uses Update and I still don't want it to play every single frame when combo %10 == 0
        // therefore we need a boolean to tell it on and off
        if ((int.Parse(this.combo_text.text) % 10 == 0) && this.ready_to_play_combo_sound == true)
        {
            this.audio_player.PlayComboUpClip();
            this.ready_to_play_combo_sound = false;
        } 

        if (old_combo_text != this.combo_text.text)
        {
            this.ready_to_play_combo_sound = true;
        }
        // Debug.Log(this.score_keeper);
        // Debug.Log(this.score_
    }

    /**
    Called by the restart button (looks like a circular arrow) to restart the game by reloading the scene
    */
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
        Debug.Log("Restarted the Game");
    }

    /**
        Called by the Main Menu button (looks like a house) to exit the runner game and go to the main menu
    */
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(this.MAIN_MENU_SCENE_NAME);
        Debug.Log("Goes to main menu scene");
    }
}
