using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCarousel : MonoBehaviour
{
    [SerializeField] public GameObject scoreAttackGame;
    [SerializeField] public GameObject timeAttackGame;
    [SerializeField] public GameObject memoryGame;
    [SerializeField] public GameObject runnerGame;

    private SpriteRenderer rend;
    private LinkedList<GameObject> carousel;


    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        GameObject[] games = {scoreAttackGame, timeAttackGame, memoryGame, runnerGame};
        carousel = new LinkedList<GameObject>(games);

        GameObject game = carousel.First.Value;
        rend.color = game.GetComponent<SpriteRenderer>().color;
        
        printCarousel();
        
    }

    [ContextMenu("Next")]
    public void nextGame() 
    {
        LinkedListNode<GameObject> oldGame = carousel.First;
        carousel.RemoveFirst();
        carousel.AddLast(oldGame);

        GameObject game = carousel.First.Value;
        rend.color = game.GetComponent<SpriteRenderer>().color;
        printCarousel();
    }

    [ContextMenu("Previous")]
    public void prevGame()
    {
        LinkedListNode<GameObject> newGame = carousel.Last;
        carousel.RemoveLast();
        carousel.AddFirst(newGame);

        GameObject game = carousel.First.Value;
        rend.color = game.GetComponent<SpriteRenderer>().color;
        printCarousel();
    }

    public void playGame()
    {

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
