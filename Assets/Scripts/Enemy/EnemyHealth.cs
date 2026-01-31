using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int _maskHealth = 3;
    [SerializeField] private EmissionFlicker _emissionFlicker;
    [SerializeField] private HitScale _hitScale;
    [SerializeField] private EnemyAttacker _enemyAttacker;

    private void OnValidate()
    {
        _emissionFlicker = GetComponent<EmissionFlicker>();
        _hitScale = GetComponent<HitScale>();
    }

    private void OnMouseEnter()
    {
        if (_enemyAttacker is EnemyMeleeAttacker enemyMeleeAttacker)
        {
            if (enemyMeleeAttacker.IsAttacking)
            {
                // take damage
            }
        }
        
        TakeMaskDamage(1);
    }

    private void TakeMaskDamage(int damage)
    {
        _emissionFlicker.Flick(Color.white, 0.15f, 50);
        _hitScale.Animate();
        ParticleSpawner.Instance.InstantiateHitParticle(transform.position);
        
        _maskHealth -= damage;
        if (_maskHealth <= 0) 
        {
            Destroy(gameObject);
            GameManager.Instance.globalEventBus.triggerEvent("SoulSaved");
        }
        
    }
}

