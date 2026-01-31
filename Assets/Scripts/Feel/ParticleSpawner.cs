using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    public static ParticleSpawner Instance;
    
    [SerializeField] private ParticleSystem _smokeParticle;
    [SerializeField] private ParticleSystem _hitParticle;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void InstantiateSmokeParticle(Vector3 position)
    {
        Instantiate(_smokeParticle, position, _smokeParticle.transform.rotation);
    }

    public void InstantiateHitParticle(Vector3 position)
    {
        Instantiate(_hitParticle, position, _hitParticle.transform.rotation);
    }
}
