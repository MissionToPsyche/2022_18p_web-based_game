using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RunnerBackgroundSpawner : MonoBehaviour
{
    [SerializeField] BackgroundWaveConfigSO current_wave;
    // [SerializeField] List<BackgroundWaveConfigSO> wave_configs;
    [SerializeField] List<GameObject> background_list;
    int index = 0;
    // [SerializeField] List<Transform> 
    // [SerializeField] float wave_delay = 1f;
    bool continue_spawning = true;

    void Start() 
    {
        // StartCoroutine(SpawnEnemyWaves());    // this is what makes the waves start
    }

    /**
        when we see the Finish tagged trigger we stop spawning meteoroids since we've reached psyche. 
    */
    // void OnTriggerEnter2D(Collider2D other) 
    // {
    //     if (other.tag == "Finish")
    //     {
    //         this.continue_spawning = false;
    //         Debug.Log("Meteoroids should stop spawning");
    //     }     
    // }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("hit trigger");
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("\nexit trigger");
        Debug.Log(other.tag);

        if (other.tag == "Respawn")
        {

            SpawnBackground();
        }
    }

    /*
    Spawns a single background off screen. this background should already be prepped to move itself
    */
    public void SpawnBackground()
    {
        
        if (this.continue_spawning == false)
        {
            return;            
        }
        GameObject background_choice = this.background_list[this.index];
        this.index++;
        // GameObject background_choice = this.background_list[0];

        // well or I could randomize it. 
        // more viable if I rotate the backgrounds to get more of them
        this.index = index % background_list.Count;
        Instantiate(background_choice, current_wave.GetStartingWaypoint().position, Quaternion.identity, transform);

    }


    /**
        returns the current wave that is being spawned
    */
    public BackgroundWaveConfigSO GetCurrentWave()
    {
        return this.current_wave;
    }

    /**
        signals the spawner to stop spawning new meteoroids
    */
    public void StopSpawningBackgrounds()
    {
        this.continue_spawning = false;

    }
}