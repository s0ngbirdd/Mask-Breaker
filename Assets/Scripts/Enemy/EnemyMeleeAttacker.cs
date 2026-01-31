using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyMeleeAttacker : EnemyAttacker
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private bool _isAttacking;
    
    public bool IsAttacking => _isAttacking;

    private void OnValidate()
    {
        _renderer = GetComponent<Renderer>();
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

            StartAttack();
            
            yield return new WaitForSeconds(1);
            
            EndAttack();
        }
    }

    public override void StartAttack()
    {
        _renderer.material.color = Color.red;
        _isAttacking = true;
    }

    public override void EndAttack()
    {
        _renderer.material.color = Color.white;
        _isAttacking = false;
    }
}
