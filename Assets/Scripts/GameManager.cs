using UnityEngine;
public enum GameState
{
    Playing,
    Won,
    GameOver
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private EnemySpawner enemySpawner;
    public GlobalEventBus globalEventBus;
    GameState currentGameState = GameState.Playing;

    public int soulsSaved = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSoulSaved()
    {
        soulsSaved++;
        Debug.Log($"Soul Saved Event Triggered: {soulsSaved}");
        if(ProgressPercentage >= 100)
        {
            setGameState(GameState.Won);
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
