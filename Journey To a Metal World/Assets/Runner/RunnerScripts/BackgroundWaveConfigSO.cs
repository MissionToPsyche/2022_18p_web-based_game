using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Background Wage Config", fileName = "New Background Wave Config")]
public class BackgroundWaveConfigSO : ScriptableObject 
{
    [SerializeField] List<GameObject> background_prefabs;
    // the list of background prefab objects that will be spawned during the game 
    // and moved around by another script 

    [SerializeField] Transform pathPrefab;
    // this will provide the path that the background will be moving along 



    /**
    get how many Game objects were put into the background prefabs serializeField 
    */
    public int GetEnemyCount()
    {
        return this.background_prefabs.Count;
    }


    /**
        returns the background prefab game object at the specified index. Note that it does not
        have a catch system in case you give an index that does not exist.  
    */
    public GameObject GetEnemyPrefab(int index)
    {
        return this.background_prefabs[index];
    }

    /**
        obtains the first waypoint 
        note: the transform Prefab represents a path in this case. 
        It has 2 children objects to represent start and 
        end points that something will be moving from and to. 
        GetChild(0) gets the first child object of the prefab object.
        this is the starting point and will be offscreen past the
         right side of the camera view)
        
        returns the Transform of this child object because that 
        is a position that can be used to know where to spawn the background
        (this is used later to know where an object should be spawned at)
    */
    public Transform GetStartingWaypoint()
    {
        return pathPrefab.GetChild(0);
    }

    /**
        See GetStartingWaypoint() for discussion of what the Transform object is for 
        my purposes in this file
        In this function rather than returning one Transform, we return the 
        list of Transform objects which gives the 2 waypoints (just think positions)
        that the background will be moving from and to
           
        returns list of waypoints in the path
    */
    public List<Transform> GetWaypoints()
    {
        List<Transform> waypoints = new List<Transform>();
        // pathPrefab was a Transform that had children objects
        // we can go through all the children objects and obtain their Transform (positions)
        // this effectively lets us create List of positions that can be used as a path
        foreach(Transform child in pathPrefab)
        {
            waypoints.Add(child);
        }
        return waypoints;
    }

}
