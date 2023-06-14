using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
    WaveConfig file for the meteoroids that will be spawned. This file identifies what meteoroid
    and which lane 
*/
[CreateAssetMenu(menuName = "Wage Config", fileName = "New Wave Config")]
public class WaveConfigSO : ScriptableObject 
{
    [SerializeField] List<GameObject> enemy_prefabs; // what meteoroid

    [SerializeField] Transform pathPrefab; // which lane

    [SerializeField] float time_between_enemy_spawns = 2f;
    [SerializeField] float spawn_time_variance = 0f;
    [SerializeField] float minimum_spawn_time = 1f;
    // RunnerMeteoroidMove meteoroid_move;


    /**
    Creates a random number that is based at the time_between_enemy_spawns variable with variance of
    spawn_time_variance. and then returns it while keeping the number within
    the minimum spawn time and the max possible float value
    */
    public float GetRandomSpawnTime()
    {
        float spawn_time = Random.Range(this.time_between_enemy_spawns - this.spawn_time_variance, 
            this.time_between_enemy_spawns + this.spawn_time_variance);
        return Mathf.Clamp(spawn_time, this.minimum_spawn_time, float.MaxValue);
    }


    /**
        Get how many prefabs (in this case meteoroids) are in the wave
    */
    public int GetEnemyCount()
    {
        return this.enemy_prefabs.Count;
    }

    /**
        return the prefab that is located at the index parameter 
    */
    public GameObject GetEnemyPrefab(int index)
    {
        return this.enemy_prefabs[index];
    }

    /**
        obtains the first waypoint to know where to start
        Returning the Transform object since that is a position
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

}
