using UnityEngine;

public class MaskCracker : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _maskSpriteRenderer;
    [SerializeField] private Sprite[] _maskCrackSprites;
    [SerializeField] private int _currentSpriteIndex = 0;
    [SerializeField] private EnemyAttacker _enemyAttacker;
    void Start()
    {
        GameManager.Instance.globalEventBus.registerEvent($"MaskDamaged:{_enemyAttacker.GetEntityId()}", ChangeCracking);
    }

    private void ChangeCracking()
    {
        _currentSpriteIndex++;
        
        if (_currentSpriteIndex < _maskCrackSprites.Length)
        {
            try
            {
                _maskSpriteRenderer.sprite = _maskCrackSprites[_currentSpriteIndex];
            }
            catch (System.Exception e)
            {
                Debug.LogError($"MaskCracker: Failed to change sprite at index {_currentSpriteIndex}. Exception: {e.Message}");
            }
        }
            
    }
}
