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

    void Awake() 
    {
        this.score_keeper = FindObjectOfType<RunnerScore>();    
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
        this.combo_text.text = RunnerMeteoroidPoints.GetCombo().ToString();
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
