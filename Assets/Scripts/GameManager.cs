
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System.Collections;
using System.Runtime.CompilerServices;
public enum GameState
{
    StartMenu,
    Playing,
    Won,
    GameOver
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private GameHUDController _gameHUDController;
    [SerializeField] private GameEndUIController _gameEndUIController;

    public GlobalEventBus globalEventBus;
    public GameState currentGameState = GameState.StartMenu;

    public int soulsSaved = 0;
    public int health = 3;
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
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
        if(currentGameState != GameState.Playing) return;
        health--;
        _gameHUDController.LoseHeart();
        globalEventBus.triggerEvent("PlayerDamaged");
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
            _gameEndUIController.setGameEndUIState(GameEndUIController.State.Won);
        } else if(currentGameState == GameState.GameOver)
        {
            Debug.Log("Game Over!");
            _gameHUDController.HideHUD();
            _gameEndUIController.setGameEndUIState(GameEndUIController.State.GameOver);
        }
    }

    public void StartGame()
    {
        currentGameState = GameState.Playing;
        soulsSaved = 0;
        health = 3;
        enemySpawner.SpawnEnemyWave();
        _gameHUDController.ShowHUD();
    }
    
    public void RestartGame()
    {
        // Destroy self before reload
        Instance = null;
        Destroy(gameObject);
        globalEventBus.ClearAllEvents();
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex
        );

    }

    public double ProgressPercentage
    {
        get
        {
            return enemySpawner.totalSouls > 0 ? (double)soulsSaved / enemySpawner.totalSouls * 100 : 0;
        }
    }
}
