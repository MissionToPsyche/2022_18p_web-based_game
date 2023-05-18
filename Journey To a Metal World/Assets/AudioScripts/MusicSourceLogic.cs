using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSourceLogic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>().updateMusicTrack();
    }
}
