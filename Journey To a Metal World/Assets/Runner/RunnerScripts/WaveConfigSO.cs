using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wage Config", fileName = "New Wave Config")]
public class WaveConfigSO : ScriptableObject 
{
    [SerializeField] Transform pathPrefab;
    [SerializeField] float move_speed = 0.3f;
    // Start is called before the first frame update

    // void Awake() 
    // {
        
    // }
    /**
    obtains the first waypoint to know where to start
    */
    public Transform GetStartingWaypoint()
    {
        return pathPrefab.GetChild(0);
    }

    /**
        returns list of waypoints in the path
    */
    public List<Transform> GetWaypoints()
    {
        List<Transform> waypoints = new List<Transform>();
        foreach(Transform child in pathPrefab)
        {
            waypoints.Add(child);
        }
        return waypoints;
    }

    public float GetMoveSpeed()
    {
        return this.move_speed;
    }
}
