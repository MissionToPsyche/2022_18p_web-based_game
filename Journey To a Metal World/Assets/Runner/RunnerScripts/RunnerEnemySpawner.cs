using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/**
    The enemy spawner in the Runner spawns meteoroids randomly after a set delay (with differing
    delays within waves and between different waves)
    It is attached to an invisible object in the game and there should be exactly one of them
    There is also a method that allows for spawning a preset arrangement of meteoroids for testing
*/
public class RunnerEnemySpawner : MonoBehaviour
{
    WaveConfigSO current_wave;
    // [SerializeField] List<WaveConfigSO> wave_configs;
    // the initial arrangement used for prototypeing 

    [SerializeField] List<WaveConfigSO> random_waves;
    // This contains every single meteoroid wave created (should be 5 lanes * 9 
    // different meteoroid sizes each for 45 different waves). And it must be in
    // the order it is beacuse otherwise the randomizer cannot consistently make sure
    // to use different lanes correctly

    [SerializeField] List<WaveConfigSO> testing_waves;
    // This one is made purely so that testing different deterministic waves can be done without
    // messing up the other specifically arranged waves

    [SerializeField] int MAX_WAVES = 90;
    // each wave has a 1 second delay and each 2nd meteoroid within a wave has a 0.3 second delay
    // this should roughly allow a full playthough to the ending to be about 2 minutes 

    [SerializeField] float wave_delay = 1f;
    [SerializeField] float WITHIN_SET_DELAY = 0.3f;
    
    RunnerProgressBar progress_bar;
    int last_lane_used = -1; // initialize it to a lane that doesn't exist
    int LANES_TOTAL = 5; // Topmost, Upper, Middle, Lower, Bottom
    int METEOROID_VARIANTS_TOTAL = 9; // 3 different sizes * 3 different meteoroids = 9 variants 

    bool continue_spawning = true;
    bool finished_all_waves = false;
    bool send_finishline = true;
    int waves_launched = 0;
    int extra_meteoroids_per_wave = 0; // will be 0 or 1
    int MINIMUM_METEOROIDS_PER_WAVE = 1;
    // theoretically more meteoroids can be send per wave like making
    // extra_meteorids_per_wave max out at 2 or 3 but then everything needs
    // to be re-tuned    
    RunnerFinishMove finishline;    

    /**
        called first
    */
    void Start() 
    {
        this.finishline = FindObjectOfType<RunnerFinishMove>();
        // we need the finishline so that we can trigger it after going through all waves

        this.progress_bar = FindObjectOfType<RunnerProgressBar>();
        // we need the progress_bar so that we can ask it to update the visual
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
        // this is the one used for the game 
        StartCoroutine(SpawnVaryingEnemyWavesRandomly());

        // this one is used for testing specific arrangements
        // StartCoroutine(TestingWavesSpacing());
    }
    
    /**
        function to take care of randomly picking a meteoroid to spawn but making 
        sure to not allow for repeatedly spawning in the same lane
        lane 0 is at the bottom. as of right now lane 4 is at the top.
        changing the number of lanes would require this to be changed 
    */
    int GetAdjustedRandomSpawn()
    {
        int lane = Random.Range(0, this.LANES_TOTAL);
        // Debug.Log("lane # " + lane);
        
        // if the same lane was used last time
        if (lane == this.last_lane_used)
        {
            // Debug.Log("dup lane");
            int coinFlip = Random.Range(0, 2);
            // then we coin flip picking the lane above or below it
            // and make sure to account for not putting it into a non existent lane
            if (coinFlip == 0)
            {
                lane -= 1;
                if (lane == -1)
                {
                    lane = 0;
                }
            }
            else
            {
                lane += 1;
                lane = lane % this.LANES_TOTAL;
            }            
        }
        this.last_lane_used = lane;

        int meteoroid_index = Random.Range(0, this.METEOROID_VARIANTS_TOTAL);

        // compute which meteoroid is sent from the 1D list 
        int index = lane * this.METEOROID_VARIANTS_TOTAL + meteoroid_index;
        // 1D List but we can treat it almost like travelling around a 2D List
        // lane number multiplied by how many in each row + the meteoroid index so we know where 
        // in that row we should be (as in column)
        
        return index;
    }


    /**
        Made for the ability to test a deterministic set of meteoroids using a very similar style to 
        what is actually used in the game. Helpful for checking spacing and dodging meteoroid feasibility
    */
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
                // bad naming to name two things waves but this one is the actual object while the loop
                // referes to wave as the total things to be instantiated at once 

                Instantiate(current_wave.GetEnemyPrefab(0),
                    current_wave.GetStartingWaypoint().position, 
                    Quaternion.identity, transform);
                yield return new WaitForSeconds(this.WITHIN_SET_DELAY);
                // 0.5 creates quite the distance. 0.1 not enough when putting thigns really close. 
                // note this was checked with gaming laptop with charger in
                //when together. the top lane and the upper lane used together must be smalls.
                // otherwise collision worries at SMALL delays 
                test_index++;
                // important to the not spawning on top of each other it seems. even if it's a smaller value 
            }
            this.extra_meteoroids_per_wave = 1;
            this.waves_launched++;

            yield return new WaitForSeconds(this.wave_delay);
        }
        if (this.send_finishline == true)
        {
            this.finishline.StartMoving();
        }

        
    }

    IEnumerator SpawnVaryingEnemyWavesRandomly()
    {
        // continuously loop spawning until you've gone through all the waves or we say to stop
        while (this.waves_launched < this.MAX_WAVES && this.continue_spawning == true)
        {
            //the actual spawning of the meteoroids from within the wave
            for (int i = 0; i < this.MINIMUM_METEOROIDS_PER_WAVE + this.extra_meteoroids_per_wave; i++)
            {
                int index = GetAdjustedRandomSpawn();
                this.current_wave = this.random_waves[index];

                // if we're supposed to stop spawning mid wave we will do so
                if (this.continue_spawning == false)
                {
                    break;
                }

                // there is only 1 meteoroid in the wave object. hence getEnemyPrefab at position 0
                // bad naming to name two things waves but this one is the actual object while the loop
                // referes to wave as the total things to be instantiated at once 
                Instantiate(current_wave.GetEnemyPrefab(0),
                    current_wave.GetStartingWaypoint().position, 
                    Quaternion.identity, transform);
                // Debug.Log(current_wave.GetStartingWaypoint().position);

                // the inner wave delay
                if (i < this.MINIMUM_METEOROIDS_PER_WAVE + this.extra_meteoroids_per_wave - 1)
                {
                    yield return new WaitForSeconds(this.WITHIN_SET_DELAY);
                }
                // important to the not spawning on top of each other it seems. even if it's a smaller value 
                // as otherwise if it's literally the same frame, the random number generator will give you
                // the same number
            }
            // bounces between 0 and 1 extra meteoroid
            this.extra_meteoroids_per_wave = (this.extra_meteoroids_per_wave + 1) % 2;
            
            this.waves_launched++;
            // increment how many waves we've launched

            float percent  = (float) (this.waves_launched) / (float) (this.MAX_WAVES);
            // the percent finished is the number of waves we launched divided by the max number of 
            // waves we can launch. then we give that to the progress bar LERP
            this.progress_bar.ProgressBarLERP(percent);

            yield return new WaitForSeconds(this.wave_delay);
            // this is the delay between waves. it's longer than the within wave delay
        }
        // if we get here we've gone through all the waves and can now send the finish condition 
        // (psyche) forward
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