/*
*   The MemoryPatternGenerator class is responsible for the generation of patterns for the memory game.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MemoryPatternGenerator : MonoBehaviour
{
    [SerializeField] float highlight_object_speed = 1.5f;
    [SerializeField] float return_to_original_color_speed = 0.5f;
    private MemoryGameController controller;
    private Queue<GameObject> pattern_storage = new Queue<GameObject>();


    /// <summary> Finds the MemoryGameController object to use its methods. </summary>
    private void Awake()
    {
        controller = FindObjectOfType<MemoryGameController>();
    }


    /// <summary> Generates a random index in the range of 0 to the length of the GameObject array (exclusive). </summary>
    /// <returns> An random integer representing an index within our array. </returns>
    private int generateRandomSolarComponentIndex()
    {
        return Random.Range(0, controller.getSolarComponentArray().Length);
    }


    /// <summary> Returns the queue of randomly generated objects. </summary>
    /// <returns> A queue holding GameObjects with the length of the difficulty index. </returns>
    public Queue<GameObject> getPatternStorage()
    {
        return pattern_storage;
    }


    /// <summary> Wipes any old patterns from our queue and starts a coroutine that generates our new pattern. </summary>
    public void generatePattern()
    {
        if (pattern_storage.Count > 0)
        {
            // when we want to generate a new set of patterns we need to clear the previous set of patterns
            pattern_storage.Clear();
        }
        StartCoroutine(displayMemoryPattern());
    }


    /// <summary> The algorithm that generates our pattern. It will enqueue a random GameObject based on
    /// our generateRandomSolarComponentIndex() method and change its color to indicate that it was chosen. </summary>
    /// <returns> An IEnumerator to pause between highlighting the object and changing its color back to normal. </returns>
    private IEnumerator displayMemoryPattern()
    {
        if (controller.getDifficultyGauge() > 1)
        {
            if (highlight_object_speed > 0.2f) // the fastest speed that the object will stay highlighted for
            {
                highlight_object_speed -= 0.05f;
            }
            if (return_to_original_color_speed > 0.15f) // the fastest speed for the next object to be selected
            {
                return_to_original_color_speed -= 0.05f;
            }
        }

        int difficulty_index = 1;
        while (difficulty_index <= controller.getDifficultyGauge())
        {
            int random_index = generateRandomSolarComponentIndex();
            GameObject solar_component = controller.getSolarComponentArray()[random_index];

            // add this to an array to verify if the player chooses the correct one.
            pattern_storage.Enqueue(solar_component);
            
            solar_component.GetComponent<Image>().color = Color.black;

            yield return new WaitForSeconds(highlight_object_speed);

            solar_component.GetComponent<Image>().color = Color.white;
            
            yield return new WaitForSeconds(return_to_original_color_speed);
            Debug.Log("object: " + solar_component);

            ++difficulty_index;
        }


        // lets us know that we can enable player click
        controller.changePatternPermissions(true);
    }
}
