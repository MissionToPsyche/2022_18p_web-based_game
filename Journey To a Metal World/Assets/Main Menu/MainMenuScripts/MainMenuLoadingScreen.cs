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
    [SerializeField] float speed;


    /// <summary> When called, this method will load the scene asynchronously, producing the loading screen. </summary>
    /// <param name="index"> An int which is the scene index to be loaded </param>
    public void loadScene(int index)
    {
        StartCoroutine(loadSceneAsync(index));
    }


    /// <summary> Enables the loading screen and will transition into the next scene. </summary>
    /// <param name="index"> An int which is the scene index to be loaded </param>
    /// <returns> IEnumerator </returns>
    private IEnumerator loadSceneAsync(int index)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);

        loading_screen_canvas.SetActive(true);

        while (!operation.isDone)
        {
            yield return new WaitForSeconds(1);
            float progress = Mathf.Clamp01(operation.progress / speed);
            loading_bar_fill.fillAmount = progress;
            // Debug.Log(progress);


            yield return null;
        }
    }
}
