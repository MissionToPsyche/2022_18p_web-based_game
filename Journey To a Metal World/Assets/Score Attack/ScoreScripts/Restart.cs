using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Restart : MonoBehaviour
{
    public void doStart(){
        StartScene.game_start = true;
    }

    public void doRestart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("Score Attack");
    }

    public void doMainMenu(){
        SceneManager.LoadScene("Main Menu");
    }
}
