using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScene : MonoBehaviour
{
    private Canvas end_canvas;
    private SpriteRenderer scene_mask;
    public static bool timeout = false;
    

    void Start()
    {
        timeout = false;
        GameObject game_object = GameObject.Find("End Scene");
        if(game_object != null){
            end_canvas = game_object.GetComponent<Canvas>();       
            end_canvas.enabled = false;
        }
    }

    /* If timeout, enable all end scene */
    void Update()
    {
        if(Timer.total_time <= 0){
            timeout = true;
            StartScene.game_start = false;
            end_canvas.enabled = true;
            
        }
    }
}
