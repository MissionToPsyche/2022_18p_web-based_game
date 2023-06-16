/* 
*   The MemoryObjectProperties class is responsible for what happens when a player clicks on one of the objects on the screen.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoryObjectProperties : MonoBehaviour
{
    [SerializeField] GameObject solar_object;
    [SerializeField] Sprite highlight_image;
    [SerializeField] AudioSource note;
    private MemoryPlayerController player_controller;
    private MemoryGameController controller;


    /// <summary> Finds the MemoryPlayerController class so that it can be used in other methods. </summary>
    void Awake()
    {
        player_controller = FindObjectOfType<MemoryPlayerController>();
        controller = FindObjectOfType<MemoryGameController>();
    }


    /// <summary> When a player clicks on a button, it will change colors to indicate that it has been pressed then change back. </summary>
    /// <returns> IEnumerator, which delays the button press color before changing back to the original color </returns>
    private IEnumerator makeSelection()
    {
        Sprite original = solar_object.GetComponent<Image>().sprite;

        solar_object.GetComponent<Image>().sprite = highlight_image;
        solar_object.GetComponent<Button>().interactable = false;
        yield return new WaitForSeconds(.3f);
        solar_object.GetComponent<Button>().interactable = true;
        solar_object.GetComponent<Image>().sprite = original;
    }


    /// <summary> This method is executed whenever the player presses the button. 
    /// It will start the coroutine using the makeSelection() method. </summary>
    public void objectSelected()
    {
        StartCoroutine(makeSelection());
    }


    /// <summary> Places the GameObject that was clicked into a queue that can be used in other scripts. </summary>
    public void storeGameObject()
    {
        // Debug.Log(solar_object + " added");
        player_controller.getPlayerSelection().Enqueue(solar_object);
    }


    /// <summary> Returns the note that is played when an object is selected. </summary>
    /// <returns> AudioSource object. </returns>
    public AudioSource getNote()
    {
        return note;
    }
}
