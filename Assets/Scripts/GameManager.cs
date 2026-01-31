
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System.Collections;
public enum GameState
{
    Playing,
    Won,
    GameOver
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private UIDocument uiDocument;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private GameHUDController _gameHUDController;

    public GlobalEventBus globalEventBus;
    GameState currentGameState = GameState.Playing;

    public int soulsSaved = 0;
    public int health = 3;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(this.gameObject);
    }
    
    void Start()
    {
        globalEventBus.registerEvent("SoulSaved", OnSoulSaved);
        globalEventBus.registerEvent("EnemyReachedEnd", OnPlayerDamaged);
    }    

    public void OnSoulSaved()
    {
        soulsSaved++;
        Debug.Log($"Soul Saved Event Triggered: {soulsSaved}");
        _gameHUDController.SetProgressAnimated((float)ProgressPercentage, 0.5f);
        if(ProgressPercentage >= 100)
        {
            setGameState(GameState.Won);
        }
    }

    public void OnPlayerDamaged()
    {
        health--;
        _gameHUDController.LoseHeart();
        Debug.Log($"Player Damaged! Health remaining: {health}");
        if(health <= 0)
        {
            setGameState(GameState.GameOver);
        }
    }

    public void setGameState(GameState newState)
    {
        currentGameState = newState;
        if(currentGameState == GameState.Won)
        {
            Debug.Log("Game Won!");
        } else if(currentGameState == GameState.GameOver)
        {
            Debug.Log("Game Over!");
        }
    }

    public double ProgressPercentage
    {
        get
        {
            return enemySpawner.totalSouls > 0 ? (double)soulsSaved / enemySpawner.totalSouls * 100 : 0;
        }
    }
}
