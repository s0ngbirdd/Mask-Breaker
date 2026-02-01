using DG.Tweening;
using UnityEngine;

public class Soul : MonoBehaviour
{
    [SerializeField] private float _maxAltitude = 10;
    [SerializeField] private float _moveSpeed = 2;

    private void OnDisable()
    {
        transform.DOKill();
    }

    private void Start()
    {
        transform.DOMove(Vector3.up * _maxAltitude, _moveSpeed).SetSpeedBased().OnComplete(() => Destroy(gameObject));
    }
}
