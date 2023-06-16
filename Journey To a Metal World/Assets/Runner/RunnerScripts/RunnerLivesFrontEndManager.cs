using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** 
    Class of methods that are involved with the visual aspect of the lives of the player
    Attached to one invisible object that is located where the live sprites should be spawned
*/
public class RunnerLivesFrontEndManager : MonoBehaviour
{

    // life_sprite will be mini images of the orbital itself
    [SerializeField] GameObject life_sprite;

    List<GameObject> life_sprite_list = new List<GameObject>();
    // this keeps tracks of the life sprites we will instantiate 

    int MAX_LIVES; // this will need to search for the value
    // from the RunnerOrbital 
    RunnerOrbital orbital;
    
    int num_lives;
    // this is the number of lives we currently have

    /**
        The first thing that happens 
        // we'll need a reference to the orbital to figure out how many lives we should start with
    */
    void Start()
    {
        orbital = FindObjectOfType<RunnerOrbital>();
        this.MAX_LIVES = orbital.GetMaxLives();
        this.num_lives = this.MAX_LIVES;
        SpawnLives(); // the function responsible for spawning life sprites 
    }


    /**
        Spawns num_lives many life_sprite with each one being 1 to the right of the previous
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
    

    /**
        destroys one visible life sprite. This method is aware of 
        there being a problem with trying to delete a life sprite that isn't there
    */
    public void DecreaseLifeSprite()
    {
        if (num_lives <= 0)
        {
            Debug.Log("already out of lives. Check for bug");
            return;
        }
        Destroy(this.life_sprite_list[this.num_lives - 1]);
        // destroys the sprites from right to left
        this.num_lives--;
    }



}