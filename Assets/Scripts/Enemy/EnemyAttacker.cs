using System.Collections;
using UnityEngine;

public abstract class EnemyAttacker : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(StartAttackCycleCoroutine());
    }

    public abstract IEnumerator StartAttackCycleCoroutine();
    
    public abstract void StartAttack();
    public abstract void EndAttack();
}
