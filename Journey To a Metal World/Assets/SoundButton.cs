using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundButton : MonoBehaviour
{
    [SerializeField] public GameObject soundUnmutedButton;
    [SerializeField] public GameObject soundMutedButton;


    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("Muted", 0) == 0) {
            soundUnmutedButton.SetActive(true);
        } else {
            soundMutedButton.SetActive(true);
        }
    }
}
