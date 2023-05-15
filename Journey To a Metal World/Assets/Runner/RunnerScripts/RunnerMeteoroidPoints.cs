using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
The entire class is really also including damage but because damamge is so short and to avoid messing with the 
already created prefabs for meteoroids late in the project it is being added here
*/
public class RunnerMeteoroidPoints : MonoBehaviour
{
    // Start is called before the first frame update
    static int combo = 1;
    static int id = 0; // this is to keep a running unique id of the meteoroids
    int unique_id = 0;  // this is to assign the unique id to the meteoroid as the static
    // field will keep increasing with every meteoroid spawned
    int point = 1; 
    int damage_amount = 1;
    bool hit_before = false;
    // in future or more complicated versions perhaps smaller meteoroids 
    // damage less and bigger ones damage more so then using an int could be expanded further
    // but for the moment damage_amount = 1 for all
    // that would require this to be made into a serialized field.
    // also... unless very obvious people wouldn't like it as much unless there were significantly more livesS
    RunnerScore score_keeper;
    void Start()
    {
        score_keeper = FindObjectOfType<RunnerScore>();
        // id is static variable 
        id++;
        this.unique_id = id;
              
    }

    /**
    Sets point total of the meteoroid to 0 (because it has been collided with)
    and resets the combo total to 0;
    */
    public void SetPointTotalToZero()
    {
        this.point = 0;
        combo = 0;
    }

    public void SetPointTotal(int newVal)
    {
        this.point = newVal;
    }

    /**
    upon reaching the points area(trigger behind meteoroids). we add a point to the running score
    the point is influenced by factors including if the meteoroid has been hit by the orbital. 
    if it has, then there is no point from passing it. This is accounted for within this 
    RunnerMeteoroidPoints class. It also makes sure you cannot accumulate points from moving back
    and forth past one meteoroid. returns the number of points added
    */
    public int ReachedPointsArea()
    {
        int added = (int) (this.point * (System.Math.Pow(2, (int)combo/10)));
        
        score_keeper.AddToScore(added);
        combo++;
        this.point  = 0;
        Debug.Log(added + "points added to score, combo is " + combo);
        return added;
        
    }


    /**
        sets the damage amount for the current meteoroid to zero
        this is intended to be used because invincibility frames don't work when the computer
        is so powerful that all frames go by too fast
    */
    public void SetHitBeforeToTrue()
    {
        this.hit_before = true;
    }

    /**
        gets the damamge_amount's integer value and returns it. 
        this will be used to figure out how many lives / health to deduct on a hit 
    */
    public int GetDamageAmount()
    {
        if (true == this.hit_before)
        {
            return 0;
        }
        this.hit_before = true;
        return this.damage_amount;
    }

    // public int EngageNoDoubleHit()
    // {
    //     this.damage_amount = 0;
    // }
    // Update is called once per frame
    // void Update()
    // {
        
    // }

    /**
    */
    public int GetUniqueId()
    {
        return this.unique_id;
    }
}
