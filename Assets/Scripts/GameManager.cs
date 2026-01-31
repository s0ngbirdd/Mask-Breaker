
using UnityEngine;
using UnityEngine.UIElements;
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
    public GlobalEventBus globalEventBus;
    GameState currentGameState = GameState.Playing;
    private ProgressBar progressBar;

    public int soulsSaved = 0;
    public int health = 3;
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
        globalEventBus.registerEvent("EnemyReachedEnd", OnPlayerDamaged);
    }


    void OnEnable()
    {
        var root = uiDocument.rootVisualElement;
        root.RegisterCallback<GeometryChangedEvent>(OnUIReady);
    }

    void OnUIReady(GeometryChangedEvent evt)
    {
        var root = uiDocument.rootVisualElement;
        root.UnregisterCallback<GeometryChangedEvent>(OnUIReady);
        
        progressBar = root.Q<ProgressBar>();
        if(progressBar == null)
        {
            Debug.LogError("ProgressBar not found in UI Document");
            return;
        }
        progressBar.value = 0f;
        progressBar.highValue = 100f;
        progressBar.lowValue = 0f; 
    }
    

    public void OnSoulSaved()
    {
        soulsSaved++;
        Debug.Log($"Soul Saved Event Triggered: {soulsSaved}");
        progressBar.value = (float)ProgressPercentage;
        if(ProgressPercentage >= 100)
        {
            setGameState(GameState.Won);
        }
    }

    public void OnPlayerDamaged()
    {
        health--;
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
