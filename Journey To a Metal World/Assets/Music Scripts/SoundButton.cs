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


    private MusicManagerLogic musicManagerLogic;


    private static SpriteState soundUnmutedPressedState;
    private static SpriteState soundMutedPressedState;
    private static float duration = 0.2f;


    void Awake()
    {
        soundUnmutedPressedState.pressedSprite = soundUnmutedPressedButton;
        soundMutedPressedState.pressedSprite = soundMutedPressedButton;
    }


    // Start is called before the first frame update
    void Start()
    {
        musicManagerLogic = GameObject.FindWithTag("Music").GetComponent<MusicManagerLogic>();
        updateIcon();
        initializeMusic();
    }


    public void toggleMusic() {
        musicManagerLogic.toggleMusic();
        updateIcon();
        updateMusic();
    }


    private void initializeMusic()
    {
        AudioListener.volume = 0;

        if (PlayerPrefs.GetInt("Muted", 0) == 0) {
            StartCoroutine(fadeInMusic());
        }
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


    private void updateMusic() {
        if (PlayerPrefs.GetInt("Muted", 0) == 0) {
            AudioListener.volume = 1;
        } else {
            AudioListener.volume = 0;
        }
    }


    private IEnumerator fadeInMusic() {
        float currentTime = 0;
        float start = AudioListener.volume;

        yield return new WaitForSeconds(0.3f);

        while (currentTime < duration) {
            if (PlayerPrefs.GetInt("Muted", 0) == 1) {
                yield break;
            }

            currentTime += Time.deltaTime;
            AudioListener.volume = Mathf.Lerp(start, 1, currentTime / duration);
            yield return null;
        }
        yield break;
    }
}
