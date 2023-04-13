using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RunnerEnemySpawner : MonoBehaviour
{
    WaveConfigSO current_wave;
    [SerializeField] List<WaveConfigSO> wave_configs;
    // [SerializeField] List<Transform> 
    [SerializeField] float wave_delay = 1f;
    bool continue_spawning = true;
    bool finished_all_waves = false;
    bool send_finishline = true;
    RunnerFinishMove finishline;

    void Start() 
    {
        this.finishline = FindObjectOfType<RunnerFinishMove>();
        StartCoroutine(SpawnEnemyWaves());    
    }

    /**
        when we see the Finish tagged trigger we stop spawning meteoroids since we've reached psyche. 
    */
    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Finish")
        {
            this.continue_spawning = false;
            Debug.Log("Meteoroids should stop spawning");
        }     
    }

    //coroutine
    IEnumerator SpawnEnemyWaves()
    {
        foreach (WaveConfigSO wave in this.wave_configs)
        {
            this.current_wave = wave;
            for (int i = 0; i < this.current_wave.GetEnemyCount(); i++)
            {
                if (this.continue_spawning == false)
                {
                    break;
                }

                Instantiate(current_wave.GetEnemyPrefab(i),
                    current_wave.GetStartingWaypoint().position, 
                    Quaternion.identity, transform);
                // break out and wait for something
                // as of right now there will only be one thing in each wave so
                //that I can not have a stream of all meteoroids in one line. will
                // need the following line if more than 1 meteoroid in a wave
                // yield return new WaitForSeconds(current_wave.GetRandomSpawnTime());
            }
            yield return new WaitForSeconds(this.wave_delay);

        }
        this.finished_all_waves = true;
        if (this.send_finishline == true)
        {
            this.finishline.StartMoving();
        }
    }


    /**
        returns the status of the finished_all_waves variable (ie. true if all waves have finished)
        otherwise it will return false
    */
    public bool GetFinishedAllWaves()
    {
        return this.finished_all_waves;
    }
    /**
        returns the current wave that is being spawned
    */
    public WaveConfigSO GetCurrentWave()
    {
        return this.current_wave;
    }

    /**
        signals the spawner to stop spawning new meteoroids
    */
    public void StopSpawningMeteoroids()
    {
        this.continue_spawning = false;
        this.send_finishline = false;

    }
}