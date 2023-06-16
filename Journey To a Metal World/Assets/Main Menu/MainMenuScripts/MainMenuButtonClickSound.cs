using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtonClickSound : MonoBehaviour
{
    [SerializeField] AudioSource click_effect;


    /// <summary> This method is linked to all buttons in the main menu. It will play a sound when something is clicked. </summary>
    public void playClick()
    {
        click_effect.Play();
    }
}
