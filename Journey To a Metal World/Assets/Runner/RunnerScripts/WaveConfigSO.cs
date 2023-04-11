using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wage Config", fileName = "New Wave Config")]
public class WaveConfigSO : ScriptableObject 
{
    [SerializeField] List<GameObject> enemy_prefabs;
    [SerializeField] Transform pathPrefab;
    [SerializeField] float move_speed = 3f;
    // note, this is positive because it is being used
    // with a path and so if it were negative it would actually go
    // the opposite direction of the next path point rather than simply to the left
    // Start is called before the first frame update

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


    // void Start() 
    // {
        // this.meteoroid_move = FindObjectOfType<RunnerMeteoroidMove>();
    // }

    public int GetEnemyCount()
    {
        return this.enemy_prefabs.Count;
    }

    public GameObject GetEnemyPrefab(int index)
    {
        return this.enemy_prefabs[index];
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

    public float GetMoveSpeed()
    {
        // return this.meteoroid_move.GetCurrentSpeed();
        return this.move_speed;
    }
}
