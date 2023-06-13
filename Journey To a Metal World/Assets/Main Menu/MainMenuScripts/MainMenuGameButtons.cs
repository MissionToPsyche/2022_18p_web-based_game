using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuGameButtons : MonoBehaviour
{
    /// <summary> This is linked to all buttons with the house icon. When pressed it will return the user to the main menu. </summary>
    public void returnToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    /// <summary> This is linked to all buttons with the restart icon. When pressed it will restart the minigame. </summary>
    public void restartMiniGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
