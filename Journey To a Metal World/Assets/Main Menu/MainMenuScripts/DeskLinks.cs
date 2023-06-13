using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeskLinks : MonoBehaviour
{
    /// <summary> Loads the About scene in the main menu. </summary>
    public void goAboutScene()
    {
        SceneManager.LoadScene("Phase A");
    }


    /// <summary> Loads the Credits scene in the main menu. </summary>
    public void goCreditsScene()
    {
        SceneManager.LoadScene("Credits");
    }
}
