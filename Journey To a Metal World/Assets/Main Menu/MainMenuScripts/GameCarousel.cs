using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameCarousel : MonoBehaviour
{
    [SerializeField] public GameObject scoreAttackGame;
    [SerializeField] public GameObject timeAttackGame;
    [SerializeField] public GameObject memoryGame;
    [SerializeField] public GameObject runnerGame;
    [SerializeField] GameObject score_attack_animation;
    [SerializeField] GameObject time_attack_animation;
    [SerializeField] GameObject memory_animation;
    [SerializeField] GameObject runner_animation;
    [SerializeField] TextMeshProUGUI game_name_and_score;

    private SpriteRenderer rend;
    private LinkedList<GameObject> carousel;

    // creates a mapping of the games array to the animated game objects
    private Dictionary<GameObject, GameObject> animation_order = new Dictionary<GameObject, GameObject>();
    private Dictionary<GameObject, string> game_name = new Dictionary<GameObject, string>();


    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        GameObject[] games = {scoreAttackGame, timeAttackGame, memoryGame, runnerGame};
        carousel = new LinkedList<GameObject>(games);

        // turning on animations
        animation_order[games[0]] = score_attack_animation;
        animation_order[games[1]] = time_attack_animation;
        animation_order[games[2]] = memory_animation;
        animation_order[games[3]] = runner_animation;

        // used before animations were created
        // GameObject game = carousel.First.Value;
        // rend.color = game.GetComponent<SpriteRenderer>().color;

        game_name[games[0]] = "Score Attack";
        game_name[games[1]] = "Mechanical Madness";
        game_name[games[2]] = "Among the Stars";
        game_name[games[3]] = "A Pebble in the Way";

        // enables the score attack animation
        animation_order[games[0]].SetActive(true);
        game_name_and_score.text = game_name[games[0]] + "\nScore: ";
        
        printCarousel();        
    }


    [ContextMenu("Next")]
    public void nextGame() 
    {
        LinkedListNode<GameObject> oldGame = carousel.First;
        carousel.RemoveFirst();
        carousel.AddLast(oldGame);
        
        // disables the current game's animation
        animation_order[oldGame.Value].SetActive(false);

        GameObject game = carousel.First.Value;

        animation_order[game].SetActive(true);
        // shows the name of the game and score on score monitor
        game_name_and_score.text = game_name[game] + "\nScore: ";

        // rend.color = game.GetComponent<SpriteRenderer>().color;
        printCarousel();
    }


    [ContextMenu("Previous")]
    public void prevGame()
    {
        LinkedListNode<GameObject> oldGame = carousel.First;
        // disables the current game's animation
        animation_order[oldGame.Value].SetActive(false);

        LinkedListNode<GameObject> newGame = carousel.Last;
        carousel.RemoveLast();
        carousel.AddFirst(newGame);

        GameObject game = carousel.First.Value;

        animation_order[game].SetActive(true);
        game_name_and_score.text = game_name[game] + "\nScore: ";
        
        //rend.color = game.GetComponent<SpriteRenderer>().color;
        printCarousel();
    }


    public void playGame()
    {
        GameObject game = carousel.First.Value;
        SceneManager.LoadScene(game.name);
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
