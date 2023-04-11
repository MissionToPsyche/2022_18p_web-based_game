using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoryVolumeControl : MonoBehaviour
{
    [SerializeField] GameObject current_button;
    [SerializeField] GameObject targeted_button;


    public void mute()
    {
        current_button.SetActive(false);
        targeted_button.SetActive(true);

        // mute code goes here
    }


    public void unmute()
    {
        current_button.SetActive(false);
        targeted_button.SetActive(true);

        // unmute code goes here
    }
}
