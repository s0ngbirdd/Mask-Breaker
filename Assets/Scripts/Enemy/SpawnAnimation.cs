using DG.Tweening;
using UnityEngine;

public class SpawnAnimation : MonoBehaviour
{
    [SerializeField] private float _moveEndValueY = 0.5f;
    [SerializeField] private float _particleSpawnOffsetY = 0.4f;
    [SerializeField] private EnemyMovement _enemyMovement;

    private Sequence _animationSequence;
    
    public float MoveEndValueY => _moveEndValueY;

    private void OnValidate()
    {
        _enemyMovement = GetComponent<EnemyMovement>();
    }

    public void Animate()
    {
        if (_animationSequence != null)
            _animationSequence.Kill(true);
        
        _animationSequence = DOTween.Sequence();
        
        _animationSequence.Append(transform.DOLocalMoveY(_moveEndValueY, 0.1f));
        _animationSequence.Join(transform.DOScale(new Vector3(1.2f, 0.8f, 1.2f), 0.15f).SetDelay(0.075f));
        _animationSequence.JoinCallback(() =>
        {
            ParticleSpawner.Instance.InstantiateSmokeParticle(transform.position - Vector3.up * _particleSpawnOffsetY);
        }).SetDelay(0.01f);
        _animationSequence.Append(transform.DOScale(1, 0.1f));
        _animationSequence.AppendCallback(() =>
        {
            _enemyMovement.StartMove();
        });
    }
}
