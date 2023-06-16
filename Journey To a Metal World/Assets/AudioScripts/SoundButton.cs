using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    [SerializeField] public Sprite soundUnmutedButton;
    [SerializeField] public Sprite soundUnmutedPressedButton;
    [SerializeField] public Sprite soundMutedButton;
    [SerializeField] public Sprite soundMutedPressedButton;


    private AudioManager audioManager;
    private static SpriteState soundUnmutedPressedState;
    private static SpriteState soundMutedPressedState;


    void Awake()
    {
        soundUnmutedPressedState.pressedSprite = soundUnmutedPressedButton;
        soundMutedPressedState.pressedSprite = soundMutedPressedButton;
    }


    void Start()
    {
        audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
        updateIcon();
    }


    public void toggleSoundButton() {
        audioManager.toggleAudio();
        updateIcon();
    }


    private void updateIcon()
    {
        if (PlayerPrefs.GetInt("Muted", 0) == 0) {
            GetComponent<Image>().sprite = soundUnmutedButton;
            GetComponent<Button>().spriteState = soundUnmutedPressedState;
        } else {
            GetComponent<Image>().sprite = soundMutedButton;
            GetComponent<Button>().spriteState = soundMutedPressedState;
        }
    }
}
