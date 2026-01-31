using DG.Tweening;
using UnityEngine;

public class HitScale : MonoBehaviour
{
    private Sequence _hitSequence;

    private void OnDisable()
    {
        transform.DOKill();
    }

    public void Animate()
    {
        transform.localScale = Vector3.one;
        
        transform.DOScale(new Vector3(1.2f, 0.8f, 1.2f), 0.15f).SetLoops(2, LoopType.Yoyo);
    }
}
