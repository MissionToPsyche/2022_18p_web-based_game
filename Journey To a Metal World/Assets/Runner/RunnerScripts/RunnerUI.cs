using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

using UnityEngine.UI;
using TMPro;

public class RunnerUI : MonoBehaviour
{
    [Header("Lives")]
    [SerializeField] Slider lives_slider;
    [SerializeField] RunnerOrbital player_lives;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI score_text;
    RunnerScore score_keeper;

    void Awake() 
    {
        score_keeper = FindObjectOfType<RunnerScore>();    
    }

     void Start() 
     {

        // player_lives.maxValue = player_lives.GetLives();    
    }

    void Update()
    {
        lives_slider.value = player_lives.GetLives();

        score_text.text = (score_keeper.GetScore()).ToString();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
        Debug.Log("Restarted the Game");
        Debug.Log("Theoretically works but may apparently be issues if there is anything marked don't destroy on load");
    }
    // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
