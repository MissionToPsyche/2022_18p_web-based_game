using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

using UnityEngine.UI;
using TMPro;

/**
    Handles the UI of the Runner Game 
    Note, the Combo amount here (11) needs to match the combo amount in RunnerPoints
    This script needs to be attached to the UI Canvas
*/
public class RunnerUI : MonoBehaviour
{
    string MAIN_MENU_SCENE_NAME = "Main Menu";

    [Header("Score")]
    [SerializeField] TextMeshProUGUI score_text;
    // needs to be given the score text object that will be modified 

    [SerializeField] TextMeshProUGUI combo_text;
    // needs to be given the combo text object that will be modified

    RunnerScore score_keeper;
    
    RunnerAudioPlayer audio_player;
    bool ready_to_play_combo_sound = false;

    int COMBO_AMOUNT = 11;

    void Awake() 
    {
        this.score_keeper = FindObjectOfType<RunnerScore>();   
        // The RunnerScore is what actually deals with managing the score while within the game 

        this.audio_player = FindObjectOfType<RunnerAudioPlayer>();
        // The audio_player is necessary for playing audio clips. In this case, the combo-up sound
    }

    /**
        SetComboToZero is a static method that makes sure combos cannot be carried over restarts 
        this edge case is due to the fact that combo was created from a static variable 
    */
    void Start() 
    {
        RunnerMeteoroidPoints.SetComboToZero();
    }

    /**
        Update the score and combo text each frame
    */
    void Update()
    {
        this.score_text.text = this.score_keeper.GetScore().ToString();
        string old_combo_text = this.combo_text.text;
        this.combo_text.text = RunnerMeteoroidPoints.GetCombo().ToString();
        // due to the fact that I don't want this to have to be checked in every meteoroid (and to avoid them from triggering
        // multiple times for one combo up, it needs to go in a script that only appears once. 
        // additionally, this script uses Update and I still don't want it to play every single frame when combo %11 == 0
        // therefore we need a boolean to tell it on and off
        // it's also static method so we can do this without needing a reference to the script
        
        // if we have a multiple of the combo amount and we're ready to play the combo sound and the combo text isn't 0 play 
        // the combo sound. 
        if ((int.Parse(this.combo_text.text) % this.COMBO_AMOUNT == 0) && this.ready_to_play_combo_sound == true && int.Parse(this.combo_text.text) != 0)
        {
            this.audio_player.PlayComboUpClip();
            this.ready_to_play_combo_sound = false; // we don't want to continuously play the sound while the score is a multiple
            // of the COMBO_AMOUNT
        } 
        // if the score has changed then we know we're not going to be continuously playing so we can once more
        // be ready to play the combo sound.
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
