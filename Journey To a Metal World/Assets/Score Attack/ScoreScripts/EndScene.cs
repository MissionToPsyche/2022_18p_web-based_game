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
        GameObject game_object_2 = GameObject.Find("End Background");
        if(game_object != null){
            end_canvas = game_object.GetComponent<Canvas>();       
            end_canvas.enabled = false;
        }
        if(game_object_2 != null){
            scene_mask = game_object_2.GetComponent<SpriteRenderer>();
            scene_mask.enabled = false;
        }
    }

    /* If timeout, enable all end scene */
    void Update()
    {
        if(Timer.total_time <= 0){
            timeout = true;
            scene_mask.enabled = true;
            end_canvas.enabled = true;
        }
    }
}
