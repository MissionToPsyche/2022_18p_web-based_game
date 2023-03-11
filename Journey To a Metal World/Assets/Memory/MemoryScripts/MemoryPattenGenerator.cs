using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MemoryPattenGenerator : MonoBehaviour
{
    [SerializeField] float highlight_object_speed = 1.5f;
    [SerializeField] float return_to_original_color_speed = 0.5f;
    private MemoryGameController controller;
    private ArrayList pattern_storage = new ArrayList();


    private void Awake()
    {
        controller = FindObjectOfType<MemoryGameController>();
    }


    private int generateRandomSolarComponentIndex()
    {
        return Random.Range(0, controller.getSolarComponentArray().Length);
    }


    public MemoryGameController GetGameController()
    {
        return controller;
    }


    public ArrayList getPatternStorage()
    {
        return pattern_storage;
    }


    public void generatePattern()
    {
        if (pattern_storage.Count > 0)
        {
            // when we want to generate a new set of patterns we need to clear the previous set of patterns
            pattern_storage.Clear();
        }
        StartCoroutine(displayMemoryPattern());
    }


    IEnumerator displayMemoryPattern()
    {
        int difficulty_index = 0;
        while (difficulty_index <= controller.getDifficultyGauge())
        {
            int random_index = generateRandomSolarComponentIndex();
            GameObject solar_component = controller.getSolarComponentArray()[random_index];

            // add this to an array to verify if the player chooses the correct one.
            pattern_storage.Add(solar_component);
            
            solar_component.GetComponent<Image>().color = Color.black;
            yield return new WaitForSeconds(highlight_object_speed);
            solar_component.GetComponent<Image>().color = Color.white;
            yield return new WaitForSeconds(return_to_original_color_speed);
            Debug.Log("object: " + solar_component);

            ++difficulty_index;
            
        }

        // might move this to when the player successfully repeats the pattern then it will get harder
        Debug.Log("\niteration: " + controller.getDifficultyGauge());
        controller.addToDifficultyGauge(1);
    }
}
