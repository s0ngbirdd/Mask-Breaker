using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private float _fps = 30;
    [SerializeField] private int _animationStep;
    [SerializeField] private float _fpsCounter;

    private void OnValidate()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _fpsCounter += Time.deltaTime;

        if (_fpsCounter >= 1 / _fps)
        {
            _animationStep++;
            
            if (_animationStep == _sprites.Length)
                _animationStep = 0;
            
            _spriteRenderer.sprite = _sprites[_animationStep];

            _fpsCounter = 0;
        }
    }
}
