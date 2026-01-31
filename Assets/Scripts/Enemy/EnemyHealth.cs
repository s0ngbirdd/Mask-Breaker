using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int _health = 3;
    [SerializeField] private EmissionFlicker _emissionFlicker;
    [SerializeField] private HitScale _hitScale;

    private void OnValidate()
    {
        _emissionFlicker = GetComponent<EmissionFlicker>();
        _hitScale = GetComponent<HitScale>();
    }

    private void OnMouseEnter()
    {
        TakeDamage(1);
    }

    private void TakeDamage(int damage)
    {
        _emissionFlicker.Flick(Color.white, 0.15f, 50);
        _hitScale.Animate();
        ParticleSpawner.Instance.InstantiateHitParticle(transform.position);
        
        _health -= damage;
        if (_health <= 0)
            Destroy(gameObject);
    }
}
