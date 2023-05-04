using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Background Wage Config", fileName = "New Background Wave Config")]
public class BackgroundWaveConfigSO : ScriptableObject 
{
    [SerializeField] List<GameObject> background_prefabs;
    [SerializeField] Transform pathPrefab;

    /**
    Creates a random number that is based at the time_between_enemy_spawns variable with variance of
    spawn_time_variance. and then returns it while keeping the number within
    the minimum spawn time and the max possible float value
    */
    // public float GetRandomSpawnTime()
    // {
    //     float spawn_time = Random.Range(this.time_between_enemy_spawns - this.spawn_time_variance, 
    //         this.time_between_enemy_spawns + this.spawn_time_variance);
    //     return Mathf.Clamp(spawn_time, this.minimum_spawn_time, float.MaxValue);
    // }


    // void Start() 
    // {
        // this.meteoroid_move = FindObjectOfType<RunnerMeteoroidMove>();
    // }

    public int GetEnemyCount()
    {
        return this.background_prefabs.Count;
    }

    public GameObject GetEnemyPrefab(int index)
    {
        return this.background_prefabs[index];
    }
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

    // public float GetMoveSpeed()
    // {
    //     // return this.meteoroid_move.GetCurrentSpeed();
    //     return this.move_speed;
    // }
}
