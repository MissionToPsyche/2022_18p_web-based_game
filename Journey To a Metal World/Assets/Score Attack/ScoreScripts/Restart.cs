using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Restart : MonoBehaviour
{
    // public static bool restart = false;
    
    // void Awake() {
    //     DontDestroyOnLoad(this.gameObject);
    // }

    // void Update()
    // {
    //     if(restart)
    //     {
    //         SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    //         restart = false;
    //     }
    // }
    public void doRestart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("Score Attack");
        Debug.Log("restart");
    }

    public void doMainMenu(){
        SceneManager.LoadScene("Main Menu");
        Debug.Log("Main Menu");
    }
}
