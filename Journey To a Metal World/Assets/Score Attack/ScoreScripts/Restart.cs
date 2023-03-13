using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Restart : MonoBehaviour
{
    public static bool restart = false;
    
    void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        if(restart)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            restart = false;
        }
    }
}
