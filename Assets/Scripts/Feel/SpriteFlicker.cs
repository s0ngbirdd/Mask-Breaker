using DG.Tweening;
using UnityEngine;

public class SpriteFlicker : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] _spriteRenderers;
    
    private Tween _tween;

    private void OnDisable()
    {
        _tween.Kill();
    }

    public void FadeFlick(Color color, float flickDuration = 0.15f)
    {
        foreach (SpriteRenderer spriteRenderer in _spriteRenderers)
            spriteRenderer.color = color;

        int timer = 1;
        _tween = DOTween.To(() => timer, x => timer = x, 0, flickDuration).OnComplete(() =>
        {
            foreach (SpriteRenderer spriteRenderer in _spriteRenderers)
                spriteRenderer.color = Color.white;
        });
    }
}
