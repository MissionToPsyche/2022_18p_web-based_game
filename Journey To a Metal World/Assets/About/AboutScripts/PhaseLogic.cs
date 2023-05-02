using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhaseLogic : MonoBehaviour
{
    [SerializeField] public string nextPhaseName = null;
    [SerializeField] public string prevPhaseName = null;


    public void nextPhase()
    {
        if (nextPhaseName != null) {
            SceneManager.LoadScene(nextPhaseName);
        }
    }


    public void prevPhase()
    {
        if (prevPhaseName != null) {
            SceneManager.LoadScene(prevPhaseName);
        }
    }

    public void exitPhase()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
