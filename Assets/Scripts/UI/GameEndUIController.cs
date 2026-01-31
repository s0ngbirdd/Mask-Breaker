using UnityEngine;
using UnityEngine.UIElements;

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
