using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhaseBLogic : MonoBehaviour
{
    [ContextMenu("Previous Phase")]
    public void prevPhase()
    {
        SceneManager.LoadScene("Phase A");
    }
}
