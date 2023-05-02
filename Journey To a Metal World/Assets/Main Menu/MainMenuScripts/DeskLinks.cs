using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeskLinks : MonoBehaviour
{
    public void goAboutScene()
    {
        SceneManager.LoadScene("Phase A");
    }


    public void goCreditsScene()
    {
        SceneManager.LoadScene("Credits");
    }
}
