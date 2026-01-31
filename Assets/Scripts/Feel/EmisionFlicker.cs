using DG.Tweening;
using UnityEngine;

public class EmissionFlicker : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;

    private Material _material;
    private Color _originalEmissionColor;
    private Color _currentColor;

    private Color CurrentColor
    {
        get => _currentColor;
        set
        {
            _currentColor = value;
            
            _material.SetColor("_EmissionColor", _currentColor);
        }
    }

    private void Awake()
    {
        _material = _renderer.material;
        _originalEmissionColor = _material.GetColor("_EmissionColor");
    }

    public void Flick(Color color, float flickDuration = 0.15f, float flickIntensity = 1)
    {
        FadeFlick(color, flickDuration, flickIntensity);
    }

    public void FlickMultiple(Color color, int count = 4, float oneFlickDuration = 0.15f, float flickIntensity = 1)
    {
        FadeFlickMultiple(color, count, oneFlickDuration, flickIntensity);
    }

    private void FadeFlick(Color color, float flickDuration, float flickIntensity)
    {
        _material.EnableKeyword("_EMISSION");

        Color finalColor = color * flickIntensity;
        
        CurrentColor = finalColor;
        
        DOTween.To(() => CurrentColor, x => CurrentColor = x, _originalEmissionColor, flickDuration);
    }

    private void FastFlick(Color color, float flickDuration, float flickIntensity)
    {
        _material.EnableKeyword("_EMISSION");
        
        Color finalColor = color * flickIntensity;

        CurrentColor = finalColor;
        
        DOVirtual.DelayedCall(flickDuration, () =>
        {
            CurrentColor = _originalEmissionColor;
        });
    }
    
    private void FadeFlickMultiple(Color color, int count, float oneFlickDuration, float flickIntensity)
    {
        _material.EnableKeyword("_EMISSION");

        Sequence flickSeq = DOTween.Sequence();

        Color finalColor = color * flickIntensity;

        for (int i = 0; i < count; i++)
        {
            flickSeq.AppendCallback(() => CurrentColor = finalColor);
            flickSeq.Append(DOTween.To(() => CurrentColor, x => CurrentColor = x, _originalEmissionColor, oneFlickDuration));
        }
    }

    private void FastFlickMultiple(Color color, int count, float oneFlickDuration, float flickIntensity)
    {
        _material.EnableKeyword("_EMISSION");

        Sequence flickSeq = DOTween.Sequence();

        Color finalColor = color * flickIntensity;

        for (int i = 0; i < count; i++)
        {
            flickSeq.AppendCallback(() => CurrentColor = finalColor);
            flickSeq.AppendInterval(oneFlickDuration);
            flickSeq.AppendCallback(() => CurrentColor = _originalEmissionColor);
            flickSeq.AppendInterval(oneFlickDuration);
        }
    }
}
