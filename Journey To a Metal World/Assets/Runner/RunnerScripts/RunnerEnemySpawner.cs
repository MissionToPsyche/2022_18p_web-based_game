using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RunnerEnemySpawner : MonoBehaviour
{
    WaveConfigSO current_wave;
    [SerializeField] List<WaveConfigSO> wave_configs;
    [SerializeField] List<WaveConfigSO> random_waves;
    // [SerializeField] List<WaveConfigSO> random_waves_order_2;
    [SerializeField] int MAX_WAVES = 60;
    // [SerializeField] List<Transform> 
    [SerializeField] float wave_delay = 1f;
    int last_sent_lane = -1;
    int LANES_TOTAL = 5;
    int METEOROID_VARIANTS_TOTAL = 9;
    bool continue_spawning = true;
    bool finished_all_waves = false;
    bool send_finishline = true;
    int waves_launched = 0;
    RunnerFinishMove finishline;

    void Start() 
    {
        this.finishline = FindObjectOfType<RunnerFinishMove>();
        // StartCoroutine(SpawnEnemyWaves());    // this is what makes the waves start
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

    /*
        public function to be used by an overall manager to start the spawning of waves
        so that waves don't spawn before the game begins (ie instructions screen)
    */
    public void StartSpawning()
    {
        // StartCoroutine(SpawnEnemyWaves());
        StartCoroutine(SpawnEnemyWavesRandomly());
    }

    
    /**
        function to take care of randomly picking a meteoroid to spawn but making 
        sure to not allow for repeatedly spawning in the same lane
        lane 0 is at the bottom. as of right now lane 4 is at the top.

    */
    int GetAdjustedRandomSpawn(int last_sent_lane)
    {
        int lane = Random.Range(0, this.LANES_TOTAL);
        if (lane == last_sent_lane)
        {
            int coinFlip = Random.Range(0, 2);
            if (coinFlip == 0)
            {
                lane -= 1;
                lane = Mathf.Clamp(lane, this.LANES_TOTAL - 1, 0);
            }
            else
            {
                lane += 1;
                lane = lane % this.LANES_TOTAL;
            }            
        }
        int meteoroid_index = Random.Range(0, this.METEOROID_VARIANTS_TOTAL);
        int index = lane * this.METEOROID_VARIANTS_TOTAL + meteoroid_index;
        // 1D List but we can treat it almost like travelling around a 2D List
        // lane # multiplied by how many in each row
        // + the meteoroid index so we know where 
        // in that row we should be (as in column)
        
        return index;

    }


    IEnumerator SpawnEnemyWavesRandomly()
    {
        // System.Random rnd = new System.Random();

        // this.wave_configs.Count()
        while (this.waves_launched < this.MAX_WAVES && this.continue_spawning == true)
        {
            // int index = Random.Range(0, this.random_waves.Count);
            int index = GetAdjustedRandomSpawn(this.last_sent_lane);
            this.last_sent_lane = index;
            this.current_wave = this.random_waves[index];
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
            this.waves_launched++;
            // rnd.next should give us a intger between the lower bound 1st parameter
            // in this case 0 (inclusive) and the upper bound 2nd parameter (exclusive)
            yield return new WaitForSeconds(this.wave_delay);
        }
        if (this.send_finishline == true)
        {
            this.finishline.StartMoving();
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