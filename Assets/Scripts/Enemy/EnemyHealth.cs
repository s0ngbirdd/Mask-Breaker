using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int _maskHealth = 3;
    [SerializeField] private EmissionFlicker _emissionFlicker;
    [SerializeField] private HitScale _hitScale;

    private void OnValidate()
    {
        _emissionFlicker = GetComponent<EmissionFlicker>();
        _hitScale = GetComponent<HitScale>();
    }

    private void OnMouseEnter()
    {
        TakeMaskDamage(1);
    }

    private void TakeMaskDamage(int damage)
    {
        _emissionFlicker.Flick(Color.white, 0.15f, 50);
        _hitScale.Animate();
        ParticleSpawner.Instance.InstantiateHitParticle(transform.position);
        
        _maskHealth -= damage;
        if (_maskhealth <= 0)
            Destroy(gameObject);
    }
}
