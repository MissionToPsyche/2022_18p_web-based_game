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

    private static SpriteState soundUnmutedPressedState;
    private static SpriteState soundMutedPressedState;

    private MusicManagerLogic musicManagerLogic;

    void Awake()
    {
        soundUnmutedPressedState.pressedSprite = soundUnmutedPressedButton;
        soundMutedPressedState.pressedSprite = soundMutedPressedButton;
    }


    // Start is called before the first frame update
    void Start()
    {
        musicManagerLogic = GameObject.FindWithTag("Music").GetComponent<MusicManagerLogic>();
        updateMusic();
    }


    public void toggleMusic() {
        musicManagerLogic.toggleMusic();
        updateMusic();
    }


    public void updateMusic() {
        if (PlayerPrefs.GetInt("Muted", 0) == 0) {
            GetComponent<Image>().sprite = soundUnmutedButton;
            GetComponent<Button>().spriteState = soundUnmutedPressedState;
            AudioListener.volume = 1;
        } else {
            GetComponent<Image>().sprite = soundMutedButton;
            GetComponent<Button>().spriteState = soundMutedPressedState;
            AudioListener.volume = 0;
        }
    }
}
