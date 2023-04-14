using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Shooting : MonoBehaviour
{    
    private GameObject prefab;
    public Rigidbody2D projectile;
    private IEnumerator coroutine;
    private bool isFire = false;

    void Update()
    {
        this.isFire = false;
    }

    void OnFire(InputValue value)
    {
        this.isFire = true;
        this.Fire();
    }

    void Fire()
    {
        if(this.isFire && StartScene.game_start == true){
            this.coroutine = this.FireContinuously();
            StartCoroutine(this.coroutine);
        }
        else{
            StopCoroutine(this.coroutine);
        }
    }

    public IEnumerator FireContinuously()
    {
        while(true)
        {
            Rigidbody2D clone;
            clone = Instantiate(projectile, transform.position, transform.rotation);
            clone.velocity = transform.up * 12;
            yield return new WaitForSeconds(10000);
        }
    }

}
