using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManagerLogic : MonoBehaviour
{
    private static MusicManagerLogic instance = null;
    public static MusicManagerLogic Instance {get { return instance; } }
    private static AudioSource audioSource;
    private static GameObject unmutedSoundButton;
    private static GameObject mutedSoundButton;

    private static bool isMuted;


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


    public void muteMusic()
    {
        PlayerPrefs.SetInt("Muted", 1);
    }


    public void unmuteMusic()
    {
        PlayerPrefs.SetInt("Muted", 0);
    }
}
