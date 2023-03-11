using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MemoryPattenGenerator : MonoBehaviour
{
    private MemoryGameController controller;
    [SerializeField] float highlight_object_speed = 1.5f;
    [SerializeField] float return_to_original_color_speed = 0.5f;


    private void Awake()
    {
        controller = FindObjectOfType<MemoryGameController>();
    }


    private void Start()
    {
        
    }


    private int generateRandomSolarComponentIndex()
    {
        return Random.Range(0, controller.getSolarComponentArray().Length);
    }


    public void generatePattern()
    {
        StartCoroutine(displayMemoryPattern());
    }


    IEnumerator displayMemoryPattern()
    {
        int difficulty_index = 0;
        while (difficulty_index <= controller.getDifficultyGauge())
        {
            int random_index = generateRandomSolarComponentIndex();
            GameObject solar_component = controller.getSolarComponentArray()[random_index];
            solar_component.GetComponent<Image>().color = Color.black;
            yield return new WaitForSeconds(highlight_object_speed);
            solar_component.GetComponent<Image>().color = Color.white;
            yield return new WaitForSeconds(return_to_original_color_speed);
            Debug.Log("object: " + solar_component);

            ++difficulty_index;
            
        }
        Debug.Log("\niteration: " + controller.getDifficultyGauge());
        controller.addToDifficultyGauge(1);
    }
}
