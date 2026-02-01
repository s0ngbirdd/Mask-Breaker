using DG.Tweening;
using UnityEngine;

public class BreakingMask : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _leftPart;
    [SerializeField] private SpriteRenderer _rightPart;
    
    private Sequence _leftPartSequence;
    private Sequence _rightPartSequence;

    private void OnDisable()
    {
        _leftPartSequence.Kill();
        _rightPartSequence.Kill();
    }

    private void Start()
    {
        _leftPartSequence = DOTween.Sequence();
        _rightPartSequence = DOTween.Sequence();
        
        _leftPartSequence.Append(_leftPart.transform.DOLocalMoveX(2, 1));
        _leftPartSequence.Join(_leftPart.transform.DOScale(0, 0.9f).SetDelay(0.1f));
        
        _rightPartSequence.Append(_rightPart.transform.DOLocalMoveX(-2, 1));
        _rightPartSequence.Join(_rightPart.transform.DOScale(0, 0.9f).SetDelay(0.1f));
        _rightPartSequence.AppendCallback(() => Destroy(gameObject));
    }
}
