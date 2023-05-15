using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuGameButtons : MonoBehaviour
{
    public void returnToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }


    public void restartMiniGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void sceneTransition()
    {
        SceneManager.LoadSceneAsync("Main Menu");
    }
}
