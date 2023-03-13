using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoryObjectProperties : MonoBehaviour
{
    [SerializeField] GameObject solar_object;
    private MemoryPlayerController player_controller;

    void Awake()
    {
        player_controller = FindObjectOfType<MemoryPlayerController>();
    }

    IEnumerator makeSelection()
    {
        solar_object.GetComponent<Image>().color = Color.magenta;
        solar_object.GetComponent<Button>().interactable = false;
        yield return new WaitForSeconds(1f);
        solar_object.GetComponent<Button>().interactable = true;
        solar_object.GetComponent<Image>().color = Color.white;
    }

    public void objectSelected()
    {
        StartCoroutine(makeSelection());
    }


    public void storeGameObject()
    {
        Debug.Log(solar_object + " added");
        player_controller.getPlayerSelection().Enqueue(solar_object);
    }
}
