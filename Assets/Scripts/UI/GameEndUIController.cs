using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

public class GameEndUIController : MonoBehaviour
{
    public enum State
    {
        Won,
        GameOver
    }
    [Header("UI Document")]
    [SerializeField] private UIDocument uiDocument;
    void OnEnable()
    {
         StartCoroutine(InitializeUI());
    }

    IEnumerator InitializeUI()
    {
        yield return null;
        var root = uiDocument.rootVisualElement;
        root.pickingMode = PickingMode.Ignore;
        
        var playAgainButton = root.Q<Button>("play-again-button");
        playAgainButton.clicked += () => 
        {
            OnPlayAgainButtonClicked();
        };

        var restartButton = root.Q<Button>("restart-game-button");
        restartButton.clicked += () => 
        {
            OnPlayAgainButtonClicked();
        };
    }

    public void OnPlayAgainButtonClicked()
    {
        GameManager.Instance.RestartGame();
    }
    

    public void setGameEndUIState(State state)
    {
        var root = uiDocument.rootVisualElement;
        
        var gameOverScreen = root.Q<VisualElement>("game-over-screen");
        var youWonScreen = root.Q<VisualElement>("game-won-screen");
        switch(state)
        {
            case State.Won:
                gameOverScreen.style.display = DisplayStyle.None;
                youWonScreen.style.display = DisplayStyle.Flex;
                break;
            case State.GameOver:
                gameOverScreen.style.display = DisplayStyle.Flex;
                youWonScreen.style.display = DisplayStyle.None;
                break;
        }
    }
}
