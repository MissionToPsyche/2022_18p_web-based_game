using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    public static bool game_start = false;
    private Canvas start_canvas;
    
    void Start()
    {
        game_start = false;
        GameObject game_object = GameObject.Find("Start Scene");

        if(game_object != null){
            start_canvas = game_object.GetComponent<Canvas>();       
            start_canvas.enabled = true;
        }
    }

    /// <summary> If timeout, enable all end scene </summary>
    void Update()
    {
        if(game_start == true){
            start_canvas.enabled = false;
        }
    }
}
