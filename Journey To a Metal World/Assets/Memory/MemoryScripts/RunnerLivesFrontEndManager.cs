using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunnerLivesFrontEndManager : MonoBehaviour
{

    // life_sprite will be mini images of the orbital itself
    [SerializeField] GameObject life_sprite;
    List<GameObject> life_sprite_list = new List<GameObject>();
    int MAX_LIVES; // this will need to search for the value
    // from the RunnerOrbital... or we make lives center around this file. 
    RunnerOrbital orbital;
    
    int num_lives;


    void Start()
    {
        orbital = FindObjectOfType<RunnerOrbital>();
        this.MAX_LIVES = orbital.GetMaxLives();
        this.num_lives = this.MAX_LIVES;
        SpawnLives();
    }


    /**
    Spawns num_lives many life_sprite with each one being 2 to the right of the previous
    one and the original appearing at the location of the object with the RunnerLivesFrontEndManager script
    attached as a component
    */
    void SpawnLives()
    {
        for (int i = 0; i < this.num_lives; i++)
        {
            Vector3 place = this.transform.position;
            place += new Vector3(i, 0, 0); // move the next sprite over to the right by 1

            this.life_sprite_list.Add(Instantiate(this.life_sprite, place, Quaternion.identity));  
            //Quaternion appears to be related to the rotation 
        }
    }
    
    // int LivesLeft()
    // {
    //     return this.num_lives;
    // }

    /**
        destroys one visible life sprite
    */
    public void DecreaseLifeSprite()
    {
        if (num_lives <= 0)
        {
            Debug.Log("already out of lives. Check for bug");
            return;
        }

        Destroy(this.life_sprite_list[this.num_lives - 1]);
        this.num_lives--;
    }



}