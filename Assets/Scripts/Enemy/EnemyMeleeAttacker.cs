using System.Collections;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyMeleeAttacker : EnemyAttacker
{
    [SerializeField] private Transform _scaleTarget;
    [SerializeField] private bool _isAttacking;
    
    private Tween _scaleTween;
    
    public bool IsAttacking => _isAttacking;

    private void OnDisable()
    {
        _scaleTween.Kill();
    }

    private void Start()
    {
        StartCoroutine(StartAttackCycleCoroutine());
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
        // ACTIVATE FIRE MASK SPRITE
        _scaleTween.Kill();
        _scaleTarget.localScale = Vector3.one;
        
        _isAttacking = true;
    }

    public override void EndAttack()
    {
        // DEACTIVATE FIRE MASK SPRITE
        
        _isAttacking = false;
    }
}
