using System.Collections;
using UnityEngine;

public abstract class EnemyAttacker : MonoBehaviour
{
    public string id = System.Guid.NewGuid().ToString();
    private void Start()
    {
        StartCoroutine(StartAttackCycleCoroutine());
    }

    public abstract IEnumerator StartAttackCycleCoroutine();
    
    public abstract void PrepareAttack();
    public abstract void StartAttack();
    public abstract void EndAttack();
}
