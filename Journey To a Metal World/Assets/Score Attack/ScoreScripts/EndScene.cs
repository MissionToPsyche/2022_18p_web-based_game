using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScene : MonoBehaviour
{
    private Canvas endCanvas;
    private SpriteRenderer scene_mask;
    public static bool timeout = false;
    

    void Start()
    {
        timeout = false;
        GameObject gameObject = GameObject.Find("Reload Canvas");
        GameObject gameObject2 = GameObject.Find("black");
        if(gameObject != null){
            endCanvas = gameObject.GetComponent<Canvas>();       
            // endCanvas.renderMode = RenderMode.ScreenSpaceOverlay;  
            endCanvas.enabled = false;
        }
        if(gameObject2 != null){
            scene_mask = gameObject2.GetComponent<SpriteRenderer>();
            scene_mask.enabled = false;
        }
    }

    void Update()
    {
        if(Timer.total_time <= 0){
            timeout = true;
            scene_mask.enabled = true;
            endCanvas.enabled = true;
            endCanvas.renderMode = RenderMode.WorldSpace;
        }
    }
}
