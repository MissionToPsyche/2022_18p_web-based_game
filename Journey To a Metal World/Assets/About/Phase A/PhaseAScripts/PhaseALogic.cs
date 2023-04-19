using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhaseALogic : MonoBehaviour
{
    [ContextMenu("Next Phase")]
    public void nextPhase()
    {
        SceneManager.LoadScene("Phase B");
    }
}