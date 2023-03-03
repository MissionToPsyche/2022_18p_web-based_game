using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenCarousel : MonoBehaviour
{
    [SerializeField] public GameObject scoreAttackScreen;
    [SerializeField] public GameObject timeAttackScreen;
    [SerializeField] public GameObject memoryScreen;
    [SerializeField] public GameObject runnerScreen;

    private SpriteRenderer rend;
    private LinkedList<GameObject> carousel;


    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        GameObject[] screens = {scoreAttackScreen, timeAttackScreen, memoryScreen, runnerScreen};
        carousel = new LinkedList<GameObject>(screens);
        
        printCarousel();
        
    }

    [ContextMenu("Next")]
    public void nextScreen() 
    {
        LinkedListNode<GameObject> oldScreen = carousel.First;
        carousel.RemoveFirst();
        carousel.AddLast(oldScreen);

        printCarousel();
    }

    [ContextMenu("Previous")]
    public void prevScreen()
    {
        LinkedListNode<GameObject> newScreen = carousel.Last;
        carousel.RemoveLast();
        carousel.AddFirst(newScreen);

        printCarousel();
    }

    void printCarousel()
    {
        string s = "[head, ";
        foreach (GameObject o in carousel) {
            s += o.name + ", ";
        }
        s += "tail]";
        Debug.Log(s);
    }
}
