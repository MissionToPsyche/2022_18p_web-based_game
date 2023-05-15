using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManagerLogic : MonoBehaviour
{
    private static MusicManagerLogic instance = null;
    private static bool isMuted;


    public static MusicManagerLogic Instance {get { return instance; } }


    // Start is called before the first frame update
    private void Awake() 
    {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            instance = this;
            PlayerPrefs.SetInt("Muted", 0);
        }
        DontDestroyOnLoad(this.gameObject);
    }


    public void toggleMusic()
    {
        if (PlayerPrefs.GetInt("Muted", 0) == 0) {
            PlayerPrefs.SetInt("Muted", 1);
        } else {
            PlayerPrefs.SetInt("Muted", 0);
        }    
    }
}
