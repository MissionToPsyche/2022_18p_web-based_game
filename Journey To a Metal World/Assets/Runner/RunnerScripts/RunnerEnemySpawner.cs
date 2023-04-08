using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RunnerEnemySpawner : MonoBehaviour
{
    [SerializeField] WaveConfigSO current_wave;

    void Start() 
    {
        StartCoroutine(SpawnEnemies());    
    }

    //coroutine
    IEnumerator SpawnEnemies()
    {
        
        for (int i = 0; i < this.current_wave.GetEnemyCount(); i++)
        {
            Instantiate(current_wave.GetEnemyPrefab(i),
                current_wave.GetStartingWaypoint().position, 
                Quaternion.identity, transform);
            // break out and wait for something

            yield return new WaitForSeconds(current_wave.GetRandomSpawnTime());
        }

    }

    public WaveConfigSO GetCurrentWave()
    {
        return current_wave;
    }
}