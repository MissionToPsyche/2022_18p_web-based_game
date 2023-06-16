using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Shooting : MonoBehaviour
{    
    private GameObject prefab;
    public Rigidbody2D projectile;
    private AudioSource source;
    private bool canFire = true;

    void Start() 
    {
        source = GameObject.FindWithTag("ray").GetComponent<AudioSource>();
    }

    void OnFire(InputValue value)
    {
        if(this.canFire && StartScene.game_start == true){
            Rigidbody2D clone;
            clone = Instantiate(projectile, transform.position, transform.rotation);
            clone.velocity = transform.up * 13;
            if(source != null){
                source.Play();
            }
            StartCoroutine(DelayCoroutine());
        }
    }

    /// <summary> This function is add delay time in between every shot </summary>
    IEnumerator DelayCoroutine()
    {
        this.canFire = false; 
        yield return new WaitForSeconds(0.4f);
        this.canFire = true; 
    }
}
