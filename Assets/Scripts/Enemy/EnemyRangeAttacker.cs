using System.Collections;
using UnityEngine;

public class EnemyRangeAttacker : EnemyAttacker
{
    [SerializeField] private Projectile _projectilePrefab;
    
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
        Projectile projectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
    }

    public override void EndAttack()
    {
        
    }
}
