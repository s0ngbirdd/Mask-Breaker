using UnityEngine;
using UnityEngine.UIElements;
public class GameStartScreenController : MonoBehaviour
{
    [Header("UI Document")]
    [SerializeField] private UIDocument uiDocument;

    private void OnEnable()
    {
        var root = uiDocument.rootVisualElement;
        var startGameButton = root.Q<Button>("start-game-button");
        startGameButton.clicked += () =>
        {
            GameManager.Instance.StartGame();
            root.style.display = DisplayStyle.None;
        };
    }
}
