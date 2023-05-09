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

    // void Fire()
    // {
    //     if(this.isFire && this.canFire && StartScene.game_start == true){
    //         Rigidbody2D clone;
    //         clone = Instantiate(projectile, transform.position, transform.rotation);
    //         clone.velocity = transform.up * 12;
    //         StartCoroutine(DelayCoroutine());
    //     }
    // }

    // void Fire()
    // {
    //     if(this.isFire && StartScene.game_start == true){
    //         this.coroutine = this.FireContinuously();
    //         StartCoroutine(this.coroutine);
    //     }
    //     else{
    //         StopCoroutine(this.coroutine);
    //     }
    // }

    // public IEnumerator FireContinuously()
    // {
    //     while(true)
    //     {
    //         Rigidbody2D clone;
    //         clone = Instantiate(projectile, transform.position, transform.rotation);
    //         clone.velocity = transform.up * 12;
    //         yield return new WaitForSeconds(10000000);
    //     }
    // }

}
