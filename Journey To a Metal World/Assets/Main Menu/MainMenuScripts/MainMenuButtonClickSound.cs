using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtonClickSound : MonoBehaviour
{
    [SerializeField] AudioSource click_effect;

    public void playClick()
    {
        click_effect.Play();
    }
}
