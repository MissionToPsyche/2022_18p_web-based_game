using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
    The entire class is points management and also including damage since damamge is so short 
    and to avoid messing with the  already created prefabs for meteoroids 
    late in the project 
    Part of every meteoroid prefab spawned
*/
public class RunnerMeteoroidPoints : MonoBehaviour
{

    static int combo = 0; // static int to keep track of the combo across all meteoroids
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
    // also... unless very obvious, people wouldn't like it as much unless there were significantly more livesS

    int COMBO_AMOUNT = 11; // Point magnitude per meteoroid pass goes up every 11 combo
    RunnerScore score_keeper;

    void Start()
    {
        score_keeper = FindObjectOfType<RunnerScore>(); //we'll need score keeper to keep track of the score
        // id is static variable 
        id++;
        this.unique_id = id; // set the unique_id
              
    }

    /**
        Sets point total of the meteoroid to 0 (because it has been collided with)
        and resets the combo total to 0
        (used for when we collide with the meteoroid)
    */
    public void SetPointTotalToZero()
    {
        this.point = 0;
        combo = 0;
    }

    /**
        In the event that we need it for something, we can set the points of a meteoroid to
        something else 
    */
    public void SetPointTotal(int newVal)
    {
        this.point = newVal;
    }

    /**
        upon reaching the points area (a trigger behind meteoroids). we add a point to the running score.
        the point is influenced by factors including if the meteoroid has been hit by the orbital. 
        if it has, then there is no point from passing it. This is accounted for within this 
        RunnerMeteoroidPoints class. Aside from that the combo system multiplies the amount of points gained
        by a power of 2 thata increases every 11 combo.
        It also makes sure you cannot accumulate points from moving back and forth past one meteoroid. 
        returns the number of points added
    */
    public int ReachedPointsArea()
    {
        // the point calculation
        int added = (int) (this.point * (System.Math.Pow(2, (int)combo/ this.COMBO_AMOUNT)));
        
        score_keeper.AddToScore(added);
        combo++; // increase the combo

        this.point  = 0; // you only gain points from the same meteoroid once
        // Debug.Log(added + "points added to score, combo is " + combo);
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
        // if we've hit the same meteoroid already we're not taking damage from it again
        // this is a countermeasure against frame rates allowing the same meteoroid to 
        // take multiple lives in 1 pass but additionally, the orbiter doesn't move fast enough
        // to let you hit a meteoroid and then with a noticeable gap, hit it again
        if (true == this.hit_before) 
        {
            return 0;
        }
        // otherwise, we haven't hit it before so flip the switch to now mark we've hit it 
        this.hit_before = true;
        // and return the damage amount associated with this meteoroid (should be 1)
        return this.damage_amount;
    }


    /**
        Getter for the int Unique Id of the meteoroid
    */
    public int GetUniqueId()
    {
        return this.unique_id;
    }

    /**
        Getter for the current combo amount 
    */
    public static int GetCombo()
    {
        return combo;
    }

    /**
        Sets the current combo amount to zero. Used for collisions
    */
    public static void SetComboToZero()
    {
        combo = 0;
    }
}
