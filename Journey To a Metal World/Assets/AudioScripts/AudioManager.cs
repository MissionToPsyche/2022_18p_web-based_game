using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance = null;
    private static AudioSource audioSource;
    private static float duration = 0.2f;


    public static AudioManager Instance {get { return instance; } }


    // Start is called before the first frame update
    private void Awake() 
    {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            instance = this;
            audioSource = GetComponent<AudioSource>();
            PlayerPrefs.SetInt("Muted", 0);
        }
        DontDestroyOnLoad(this.gameObject);
    }


    public void updateMusicTrack()
    {
        AudioSource musicSource = GameObject.FindWithTag("MusicSource").GetComponent<AudioSource>();
        if (audioSource.clip != musicSource.clip) {
            AudioListener.volume = 0;

            audioSource.Stop();
            audioSource.clip = musicSource.clip;
            audioSource.Play();

            if (PlayerPrefs.GetInt("Muted", 0) == 0) {
                StartCoroutine(fadeInMusic());
            }
        }
    }


    public void toggleAudio()
    {
        if (PlayerPrefs.GetInt("Muted", 0) == 0) {
            PlayerPrefs.SetInt("Muted", 1);
            AudioListener.volume = 0;
        } else {
            PlayerPrefs.SetInt("Muted", 0);
            AudioListener.volume = 1;
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
