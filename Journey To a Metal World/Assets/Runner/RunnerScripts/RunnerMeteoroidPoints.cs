using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// point system broken. something went wrong!
public class RunnerMeteoroidPoints : MonoBehaviour
{
    // Start is called before the first frame update
    int point = 1;
    RunnerScore score_keeper;
    void Start()
    {
        score_keeper = FindObjectOfType<RunnerScore>();    
       
    }
    public void SetPointTotalToZero()
    {
        this.point = 0;
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
        int added = this.point;
        score_keeper.AddToScore(this.point);
        this.point  = 0;
        Debug.Log(added + "points added to score");
        return added;
        
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }
}

