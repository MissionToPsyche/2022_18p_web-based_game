using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/**
    Responsible for spawning the background prefabs in the correct location
    and at the right time. This script should be attached to one invisible 
    object in the game 
*/
public class RunnerBackgroundSpawner : MonoBehaviour
{
    [SerializeField] BackgroundWaveConfigSO current_wave;
    // the wave has the prefabs and the path 

    [SerializeField] List<GameObject> background_list;
    // this stores the prefabs for the backgrounds. Currently there are 3. 

    int index = 0;
    bool continue_spawning = true;
    // boolean that lets us know if we are supposed to be spawning meteoroids or not. 


    /**
        The backgrounds are all triggers, when they hit something with a collider
        that has the tag respawn, that is the indication that a new background should be spawned
        This object is marked as the "BackgroundEndPoint" in the Unity Editor Hierarchy
        My speculation is that this happens in part due to the spawned meteoroids being children
        of the file that has the RunnerBackgroundSpawner script attached to it so this still manages
        to trigger.
    */
    void OnTriggerExit2D(Collider2D other)
    {
        // Debug.Log("\n exit trigger");
        // Debug.Log(other.tag);
        // trigger the respawn when an object (with a collider) exits the collision and has the tag "Respawn"
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
        // we cycle through the list of backgrounds rather than running through them randomly to 
        // make no chance of showing the same background repeatedly

        this.index = index % background_list.Count;
        Instantiate(background_choice, current_wave.GetStartingWaypoint().position, Quaternion.identity, transform);
        // this is what actually Spawns the background at the correct spot
    }


    /**
        returns the current wave that is being spawned
    */
    public BackgroundWaveConfigSO GetCurrentWave()
    {
        return this.current_wave;
    }

    /**
        tells the spawner to stop spawning new meteoroids
    */
    public void StopSpawningBackgrounds()
    {
        this.continue_spawning = false;

    }
}