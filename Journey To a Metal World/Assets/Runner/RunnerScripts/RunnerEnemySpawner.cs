using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RunnerEnemySpawner : MonoBehaviour
{
    WaveConfigSO current_wave;
    [SerializeField] List<WaveConfigSO> wave_configs;
    [SerializeField] List<WaveConfigSO> random_waves;
    [SerializeField] List<WaveConfigSO> testing_waves;
    [SerializeField] int MAX_WAVES = 60;
    // [SerializeField] List<Transform> 
    [SerializeField] float wave_delay = 1f;
    [SerializeField] float WITHIN_SET_DELAY = 0.3f;
    int last_lane_used = -1;
    int LANES_TOTAL = 5;
    int METEOROID_VARIANTS_TOTAL = 9;
    bool continue_spawning = true;
    bool finished_all_waves = false;
    bool send_finishline = true;
    int waves_launched = 0;
    int extra_meteoroids_per_wave = 0; // will be 0, 1, 2 
    int MINIMUM_METEOROIDS_PER_WAVE = 1;
    // with baseline always at least 1 meteoroid sent + 0,1,2 extra sent
    //... well as is I can do 1 extra or 0 extra fine. the problem will be 2 extra requires extra tracking. 
    // would use a stack. because toherwise like lane 0, 1, 0. not consecutive but spawns 2 lane 0's on each other 

    RunnerFinishMove finishline;

    void Awake()
    {
        // Random.InitState(System.DateTime.Now.Millisecond); 
    }
    

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
            // Debug.Log("Meteoroids should stop spawning");
        }     
    }

    /*
        public function to be used by an overall manager to start the spawning of waves
        so that waves don't spawn before the game begins (ie instructions screen)
    */
    public void StartSpawning()
    {
        // StartCoroutine(SpawnEnemyWaves());
        // StartCoroutine(SpawnEnemyWavesRandomly());
        StartCoroutine(SpawnVaryingEnemyWavesRandomly());
        // StartCoroutine(TestingWavesSpacing());
    }

    //*** the problem is not that they arne't spawning right or at least half the problem isn't that. 
    // the issue is that they are already over lapping so that means they might collide further!
    
    /**
        function to take care of randomly picking a meteoroid to spawn but making 
        sure to not allow for repeatedly spawning in the same lane
        lane 0 is at the bottom. as of right now lane 4 is at the top.

    */
    int GetAdjustedRandomSpawn()
    {
        int lane = Random.Range(0, this.LANES_TOTAL);
        // int lane = (int) (Random.value * (this.LANES_TOTAL -1));
        Debug.Log("lane # " + lane);
        if (lane == this.last_lane_used)
        {
            Debug.Log("dup lane");
            int coinFlip = Random.Range(0, 2);
            if (coinFlip == 0)
            {
                lane -= 1;
                if (lane == -1)
                {
                    lane = 0;
                }
                // lane = Mathf.Clamp(lane, this.LANES_TOTAL - 1, 0);
            }
            else
            {
                lane += 1;
                lane = lane % this.LANES_TOTAL;
            }            
        }
        this.last_lane_used = lane;

        int meteoroid_index = Random.Range(0, this.METEOROID_VARIANTS_TOTAL);
        int index = lane * this.METEOROID_VARIANTS_TOTAL + meteoroid_index;
        // 1D List but we can treat it almost like travelling around a 2D List
        // lane # multiplied by how many in each row
        // + the meteoroid index so we know where 
        // in that row we should be (as in column)
        
        return index;

    }
    IEnumerator TestingWavesSpacing()
    {
        // System.Random rnd = new System.Random();

        // this.wave_configs.Count()
        int test_index = 0;
        while (this.waves_launched < this.testing_waves.Count && this.continue_spawning == true)
        {
            for (int i = 0; i < this.MINIMUM_METEOROIDS_PER_WAVE + this.extra_meteoroids_per_wave; i++)
            {
                this.current_wave = this.testing_waves[test_index];
                Debug.Log("first Index " + test_index);

                if (this.continue_spawning == false)
                {
                    break;
                }
                // there is only 1 meteoroid in the wave object. hence getEnemyPrefab at position 0
                // bad naming to name two thigns waves but this one is the actual object while the loop
                // referes to wave as the total things to be instantiated at once 
                Instantiate(current_wave.GetEnemyPrefab(0),
                    current_wave.GetStartingWaypoint().position, 
                    Quaternion.identity, transform);
                yield return new WaitForSeconds(this.WITHIN_SET_DELAY);
                // 0.5 creates quite the distance. 0.1 not enough when putting thigns reallyh close. 
                // note this was checked with gaming laptop with charger in
                //when together. the top lane and the upper lane used together must be smalls.otherwise collision worries at SMALL delays 
                test_index++;
                // important to the not spawning on top of each other it seems. even if it's a smaller value 
            }
            this.extra_meteoroids_per_wave = 1;
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




    IEnumerator SpawnVaryingEnemyWavesRandomly()
    {
        // System.Random rnd = new System.Random();

        // this.wave_configs.Count()
        while (this.waves_launched < this.MAX_WAVES && this.continue_spawning == true)
        {
            // int index = Random.Range(0, this.random_waves.Count);

            // int secondary_index = GetAdjustedRandomSpawn(this.last_lane_used);
            // WaveConfigSO pair_wave = this.random_waves[secondary_index];
            // // this.last_lane_used = secondary_index;            
            // Debug.Log("second Index " + index);
            //Sidenote: there really should be only 1 meteoroid in the wave with the way
            // I have it written and this is because the wave must follow the path so sending 5
            // meteoroids in the same lane is kind of pointlessly weird. But done like this because
            // it works and in case I find a way to get one wave to have multi directions
            // for (int i = 0; i < this.current_wave.GetEnemyCount(); i++)
            
            for (int i = 0; i < this.MINIMUM_METEOROIDS_PER_WAVE + this.extra_meteoroids_per_wave; i++)
            {
                int index = GetAdjustedRandomSpawn();
                // this.last_lane_used = index;
                // ahhh actually. that's wrong. because I gave it the end index rather than the lane. 
                // redo it inside the function probably is best. 
                Debug.Log("first Index " + index);
                this.current_wave = this.random_waves[index];

                if (this.continue_spawning == false)
                {
                    break;
                }
                // there is only 1 meteoroid in the wave object. hence getEnemyPrefab at position 0
                // bad naming to name two thigns waves but this one is the actual object while the loop
                // referes to wave as the total things to be instantiated at once 
                Instantiate(current_wave.GetEnemyPrefab(0),
                    current_wave.GetStartingWaypoint().position, 
                    Quaternion.identity, transform);
                // Debug.Log(current_wave.GetStartingWaypoint().position);

                // Instantiate(pair_wave.GetEnemyPrefab(i),
                //     pair_wave.GetStartingWaypoint().position, 
                //     Quaternion.identity, transform);
                // Debug.Log(pair_wave.GetStartingWaypoint().position);

                // break out and wait for something
                // as of right now there will only be one thing in each wave so
                //that I can not have a stream of all meteoroids in one line. will
                // need the following line if more than 1 meteoroid in a wave
                yield return new WaitForSeconds(this.WITHIN_SET_DELAY);
                // important to the not spawning on top of each other it seems. even if it's a smaller value 
            }
            // this.extra_meteoroids_per_wave = (this.extra_meteoroids_per_wave + 1) % 2;
            this.extra_meteoroids_per_wave = 1;
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



    IEnumerator SpawnEnemyWavesRandomly()
    {
        // System.Random rnd = new System.Random();

        // this.wave_configs.Count()
        while (this.waves_launched < this.MAX_WAVES && this.continue_spawning == true)
        {
            // int index = Random.Range(0, this.random_waves.Count);
            int index = GetAdjustedRandomSpawn();
            // this.last_lane_used = index;
            // ahhh actually. that's wrong. because I gave it the end index rather than the lane. 
            // redo it inside the function probably is best. 
            Debug.Log("first Index " + index);
            this.current_wave = this.random_waves[index];

            int secondary_index = GetAdjustedRandomSpawn();
            WaveConfigSO pair_wave = this.random_waves[secondary_index];
            // // this.last_lane_used = secondary_index;            
            // Debug.Log("second Index " + index);
            //Sidenote: there really should be only 1 meteoroid in the wave with the way
            // I have it written and this is because the wave must follow the path so sending 5
            // meteoroids in the same lane is kind of pointlessly weird. But done like this because
            // it works and in case I find a way to get one wave to have multi directions
            for (int i = 0; i < this.current_wave.GetEnemyCount(); i++)
            {
                if (this.continue_spawning == false)
                {
                    break;
                }
                Instantiate(current_wave.GetEnemyPrefab(i),
                    current_wave.GetStartingWaypoint().position, 
                    Quaternion.identity, transform);
                Debug.Log(current_wave.GetStartingWaypoint().position);

                Instantiate(pair_wave.GetEnemyPrefab(i),
                    pair_wave.GetStartingWaypoint().position, 
                    Quaternion.identity, transform);
                Debug.Log(pair_wave.GetStartingWaypoint().position);

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