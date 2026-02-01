using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int _maskHealth = 3;
    [SerializeField] private SpriteFlicker _spriteFlicker;
    [SerializeField] private HitScale _hitScale;
    [SerializeField] private EnemyAttacker _enemyAttacker;
    [SerializeField] private Soul _soulPrefab;
    [SerializeField] private BreakingMask _breakingMaskPrefab;

    private void OnValidate()
    {
        _spriteFlicker = GetComponent<SpriteFlicker>();
        _hitScale = GetComponent<HitScale>();
    }

    private void OnMouseEnter()
    {
        if(GameManager.Instance.currentGameState != GameState.Playing) return;
        if (_enemyAttacker is EnemyMeleeAttacker enemyMeleeAttacker)
        {
            if (enemyMeleeAttacker.IsAttacking)
            {
                CursorController.Instance.SetDamagedCursor();
                GameManager.Instance.OnPlayerDamaged();
            } 
        }

        AudioController.Instance.PlaySlapOneShot();
        CursorController.Instance.SetSlapCursor();
        TakeMaskDamage(1);
    }

    private void TakeMaskDamage(int damage)
    {
        _spriteFlicker.FadeFlick(Color.yellow);
        _hitScale.Animate();
        ParticleSpawner.Instance.InstantiateHitParticle(transform.position);
        
        _maskHealth -= damage;
        GameManager.Instance.globalEventBus.triggerEvent($"MaskDamaged:{_enemyAttacker.GetEntityId()}");
        if (_maskHealth <= 0) 
        {
            Instantiate(_soulPrefab, transform.position, Quaternion.identity);
            Instantiate(_breakingMaskPrefab, transform.position, Quaternion.identity);
            
            Destroy(gameObject);
            GameManager.Instance.globalEventBus.triggerEvent("SoulSaved");
        }
        
    }
}

