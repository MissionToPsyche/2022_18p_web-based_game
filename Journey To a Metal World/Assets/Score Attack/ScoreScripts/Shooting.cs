using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Shooting : MonoBehaviour
{    
    private GameObject prefab;
    public Rigidbody2D projectile;
    private bool canFire = true;


    void OnFire(InputValue value)
    {
        if(this.canFire && StartScene.game_start == true){
            Rigidbody2D clone;
            clone = Instantiate(projectile, transform.position, transform.rotation);
            clone.velocity = transform.up * 13;
            StartCoroutine(DelayCoroutine());
        }
    }

    

    IEnumerator DelayCoroutine()
    {
        this.canFire = false; 
        yield return new WaitForSeconds(0.4f);
        this.canFire = true; 
    }

}
