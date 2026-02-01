using System.Collections;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyMeleeAttacker : EnemyAttacker
{
    [SerializeField] private Transform _scaleTarget;
    [SerializeField] private bool _isAttacking;
    [SerializeField] private SpriteRenderer _baseMask;
    [SerializeField] private SpriteRenderer _fireMask;
    
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
        _scaleTween.Kill();
        _scaleTarget.localScale = Vector3.one;
        
        _isAttacking = true;
        _baseMask.gameObject.SetActive(false);
        _fireMask.gameObject.SetActive(true);
    }

    public override void EndAttack()
    {
        _isAttacking = false;
        _fireMask.gameObject.SetActive(false);
        _baseMask.gameObject.SetActive(true);
    }
}
