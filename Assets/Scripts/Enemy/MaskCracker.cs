using UnityEngine;

public class MaskCracker : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _maskSpriteRenderer;
    [SerializeField] private Sprite[] _maskCrackSprites;
    [SerializeField] private int _currentSpriteIndex;

    private void ChangeCracking()
    {
        _currentSpriteIndex++;
        
        if (_currentSpriteIndex <= _maskCrackSprites.Length)
            _maskSpriteRenderer.sprite = _maskCrackSprites[_currentSpriteIndex];
    }
}
