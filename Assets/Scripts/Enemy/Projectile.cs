using DG.Tweening;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 2;
    
    private void Start()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out RaycastHit hit))
            transform.DOMove(new Vector3(hit.point.x, transform.position.y, hit.point.z), _moveSpeed).SetEase(Ease.Linear).SetSpeedBased().OnComplete(() => Destroy(gameObject));
    }

    private void OnMouseEnter()
    {
        GameManager.Instance.OnPlayerDamaged();
        Destroy(this.gameObject);
    }
}
