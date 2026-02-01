using DG.Tweening;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 2;
    [SerializeField] private Transform _targetRotation;
    
    private void Start()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            transform.DOMove(new Vector3(hit.point.x, transform.position.y, hit.point.z), _moveSpeed).SetEase(Ease.Linear).SetSpeedBased().OnComplete(() => Destroy(gameObject));
            
            Vector3 direction = (new Vector3(hit.point.x, transform.position.y, hit.point.z) - transform.position).normalized;
            _targetRotation.localRotation = Quaternion.LookRotation(-Vector3.up, -direction);
        }
    }

    private void OnMouseEnter()
    {
        CursorController.Instance.SetDamagedCursor();
        GameManager.Instance.OnPlayerDamaged();
        Destroy(this.gameObject);
    }
}
