using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int _maskHealth = 3;
    [SerializeField] private SpriteFlicker _spriteFlicker;
    [SerializeField] private HitScale _hitScale;
    [SerializeField] private EnemyAttacker _enemyAttacker;

    private void OnValidate()
    {
        _spriteFlicker = GetComponent<SpriteFlicker>();
        _hitScale = GetComponent<HitScale>();
    }

    private void OnMouseEnter()
    {
        if (_enemyAttacker is EnemyMeleeAttacker enemyMeleeAttacker)
        {
            if (enemyMeleeAttacker.IsAttacking)
            {
                GameManager.Instance.OnPlayerDamaged();
            } 
        }
        
        TakeMaskDamage(1);
    }

    private void TakeMaskDamage(int damage)
    {
        _spriteFlicker.FadeFlick(Color.yellow);
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

