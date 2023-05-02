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
    [SerializeField] TextMeshProUGUI game_names;
    [SerializeField] TextMeshProUGUI game_score;
    [SerializeField] TextMeshProUGUI high_score;

    private SpriteRenderer rend;
    private LinkedList<GameObject> carousel;

    // creates a mapping of the games array to the animated game objects
    private Dictionary<GameObject, GameObject> animation_order = new Dictionary<GameObject, GameObject>();
    private Dictionary<GameObject, string> game_name_dict = new Dictionary<GameObject, string>();
    private Dictionary<GameObject, int> game_scores = new Dictionary<GameObject, int>();


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

        game_name_dict[games[0]] = "Scan at Will!";
        game_name_dict[games[1]] = "Mechanical Madness";
        game_name_dict[games[2]] = "Among the Stars";
        game_name_dict[games[3]] = "A Pebble in the Way";

        // conditional operators that checks whether the player prefs key exists or not
        game_scores[games[0]] = PlayerPrefs.HasKey("score_attack_high_score") ? PlayerPrefs.GetInt("score_attack_high_score") : 0;
        game_scores[games[1]] = PlayerPrefs.HasKey("time_attack_high_score") ? PlayerPrefs.GetInt("time_attack_high_score") : 0;
        game_scores[games[2]] = PlayerPrefs.HasKey("memory_high_score") ? PlayerPrefs.GetInt("memory_high_score") : 0;
        game_scores[games[3]] = PlayerPrefs.HasKey("Runner_high_score") ? PlayerPrefs.GetInt("Runner_high_score") : 0;

        // enables the score attack animation
        animation_order[games[0]].SetActive(true);
        // displays "Scan at Will! on startup
        game_names.text = game_name_dict[games[0]];
        // displays Scan at Will!'s high score on startup
        game_score.text = "Best: " + game_scores[games[0]].ToString();
        
        printCarousel();        
    }


    void Update()
    {
        updateHighScore();
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
        game_names.text = game_name_dict[game];
        game_score.text = "Best: " + game_scores[game].ToString();

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
        game_names.text = game_name_dict[game];
        game_score.text = "Best: " + game_scores[game].ToString();
        
        //rend.color = game.GetComponent<SpriteRenderer>().color;
        printCarousel();
    }


    public void playGame()
    {
        GameObject game = carousel.First.Value;
        SceneManager.LoadScene(game.name);
    }


    private void updateHighScore()
    {
        GameObject[] games = {scoreAttackGame, timeAttackGame, memoryGame, runnerGame};
        int score = game_scores[games[0]] + game_scores[games[1]] + game_scores[games[2]] + game_scores[games[3]];
        high_score.text = score.ToString();
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
