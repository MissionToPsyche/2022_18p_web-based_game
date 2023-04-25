using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    private Canvas start_canvas;
    public static bool game_start = false;
    
    void Start()
    {
        game_start = false;
        GameObject game_object = GameObject.Find("Start Scene");

        if(game_object != null){
            start_canvas = game_object.GetComponent<Canvas>();       
            start_canvas.enabled = true;
        }
        
    }

    /* If timeout, enable all end scene */
    void Update()
    {
        if(game_start == true){
            start_canvas.enabled = false;
        }
    }
}
