using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    // Script references
    [SerializeField] private GameObject puck;
    [SerializeField] private CollectibleSpawn collectibleSpawn;
    [SerializeField] private PaddleBehaviour paddleBehaviour;
    [SerializeField] private OpponentBehaviour opponentBehaviour;
    [SerializeField] private DirectionChanger directionChanger;
    
    [SerializeField] private Vector2 spawnP1;
    [SerializeField] private Vector2 spawnP2;

    private Rigidbody2D puckBody;
    private SpriteRenderer puckSprite;
    private Puck puckScript;
    
    // Text references
    public TMP_Text ScoreText;
    public TMP_Text CollectibleP1;
    public TMP_Text CollectibleP2;
    public TMP_Text CountDown;
    public TMP_Text Title;
    public TMP_Text WinnerText;
    public GameObject StartButton;
    
    private Player p1;
    private Player p2;

    private bool isGameStart;
    private bool isTimerFinished;
    private float startTimer = 0;

    // Start is called before the first frame update
    void Awake()
    {
        puckBody = puck.GetComponent<Rigidbody2D>();
        puckScript = puck.GetComponent<Puck>();
        puckSprite = puck.GetComponent<SpriteRenderer>();
        
        CountDown.text = "";
        WinnerText.text = "Press to Start";
    }

    private void Start()
    {
        ResetGame();
    }

    void Update()
    {
        if (isGameStart && !isTimerFinished)
        {
            startTimer += Time.deltaTime;
            if (startTimer > 5)
            {
                isTimerFinished = true;
                CountDown.text = "";
                startTimer = 0;
                GameStart();
            }
            else CountDown.text = $"{5 - (int)startTimer}";
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetGame();
        }

        // Remove test code
        if (Input.GetKeyDown(KeyCode.Space))
        {
            puckBody.velocity = Vector2.zero;
            puckBody.position = spawnP1;
        }
    }

    private void GameStart()
    {
        paddleBehaviour.enabled = true;
        collectibleSpawn.enabled = true;
        opponentBehaviour.enabled = true;
        directionChanger.enabled = true;
    }

    void ResetGame()
    {
        isGameStart = false;
        isTimerFinished = false;
        startTimer = 0;
        
        p1 = new Player();
        p2 = new Player();
        
        // reset puck
        puckBody.velocity = Vector2.zero;
        puckBody.position = spawnP1;
        puckSprite.color = Color.grey;
        puckScript.p1HitLast = puckScript.p1HitLast = false;
        
        // reset UI
        ScoreText.text = $"{p1.getScore()} - {p2.getScore()}";
        CollectibleP1.text = $"{p1.getCollectibles()} of 3";
        CollectibleP2.text = $"{p2.getCollectibles()} of 3";
        Title.enabled = true;
        StartButton.SetActive(true);

        // reset objects and variables
        collectibleSpawn.Reset();
        paddleBehaviour.Reset();
        opponentBehaviour.Reset();
        directionChanger.Reset();

        // disable scripts
        paddleBehaviour.enabled = false;
        collectibleSpawn.enabled = false;
        opponentBehaviour.enabled = false;
        directionChanger.enabled = false;
    }

    public void GoalScored(int goal)
    {
        if (goal == 1) // point to P2
        {
            p2.Goal();
            puckBody.velocity = Vector2.zero;
            puckBody.position = spawnP1;
        }
        if (goal == 2) // point to P1
        {
            p1.Goal();
            puckBody.velocity = Vector2.zero;
            puckBody.position = spawnP2;
        }
        
        puckSprite.color = Color.grey;
        puckScript.p1HitLast = puckScript.p1HitLast = false;
        ScoreText.text = $"{p1.getScore()} - {p2.getScore()}";
        
        WinnerCheck();
    }

    void WinnerCheck()
    {
        if (p1.getScore() >= 5) // player 1 wins
        {
            WinnerText.enabled = true;
            WinnerText.text = "YOU WIN!";
            ResetGame();
        }
        if (p2.getScore() >= 5) // player 2 wins
        {
            WinnerText.enabled = true;
            WinnerText.text = "YOU LOSE";            
            ResetGame();
        }
    }

    public void PickupCollectible(int player)
    {
        if (player == 1)
        {
            p1.Collect();
            CollectibleP1.text = $"{p1.getCollectibles()} of 3";
        }
        if (player == 2)
        {
            p2.Collect();
            CollectibleP2.text = $"{p2.getCollectibles()} of 3";
        }
        
        ScoreText.text = $"{p1.getScore()} - {p2.getScore()}";

        WinnerCheck();
    }

    public void OnButtonPress()
    {
        isGameStart = true;
        
        StartButton.SetActive(false);
        Title.enabled = false;
        WinnerText.enabled = false;
    }

    public void DoubleTouch(int player)
    {
        if (player == 1)
        {
            p1.losePoint();
            ScoreText.text = $"{p1.getScore()} - {p2.getScore()}";
        }

        if (player == 2)
        {
            p2.losePoint();
            ScoreText.text = $"{p1.getScore()} - {p2.getScore()}";
        }
    }
}
