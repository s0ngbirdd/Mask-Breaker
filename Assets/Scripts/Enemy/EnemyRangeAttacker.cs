using System.Collections;
using DG.Tweening;
using UnityEngine;

public class EnemyRangeAttacker : EnemyAttacker
{
    [SerializeField] private Transform _scaleTarget;
    [SerializeField] private Projectile _projectilePrefab;

    private Tween _scaleTween;
    
    private void OnDisable()
    {
        _scaleTween.Kill();
    }
    
    public override IEnumerator StartAttackCycleCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1f, 5f));
            
            PrepareAttack();
            
            yield return new WaitForSeconds(1);

            StartAttack();
            
            yield return new WaitForSeconds(1);
            
            EndAttack();
        }
    }

    public override void PrepareAttack()
    {
        _scaleTween = _scaleTarget.DOScale(1.15f, 0.2f).SetLoops(-1, LoopType.Yoyo);
    }

    public override void StartAttack()
    {
        _scaleTween.Kill();
        _scaleTarget.localScale = Vector3.one;
        
        Projectile projectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
    }

    public override void EndAttack()
    {
        
    }
}
