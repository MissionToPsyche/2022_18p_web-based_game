using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSourceLogic : MonoBehaviour
{
    /// <summary> Replaces the audio clip being played by the AudioManager </summary>
    void Start()
    {
        GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>().updateMusicTrack();
    }
}
