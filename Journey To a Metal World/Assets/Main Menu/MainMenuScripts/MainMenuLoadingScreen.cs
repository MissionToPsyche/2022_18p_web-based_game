using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuLoadingScreen : MonoBehaviour
{
    [SerializeField] GameObject loading_screen_canvas;
    [SerializeField] Image loading_bar_fill;
    [SerializeField] Image progress1;
    [SerializeField] Image progress2;
    [SerializeField] Image progress3;
    [SerializeField] float speed;


    public void loadScene(int index)
    {
        StartCoroutine(loadSceneAsync(index));
    }


    private IEnumerator loadSceneAsync(int index)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);

        loading_screen_canvas.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / speed);
            loading_bar_fill.fillAmount = progress;

            if (progress == 50)
            {
                progress1.gameObject.SetActive(false);
                progress2.gameObject.SetActive(true);
            }

            if (progress == 80)
            {
                progress2.gameObject.SetActive(false);
                progress3.gameObject.SetActive(true);
            }

            yield return new WaitForSecondsRealtime(4f);
        }
    }
}
