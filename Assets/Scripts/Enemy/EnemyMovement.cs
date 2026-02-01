using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeedMultiplier = 0.5f;
    [SerializeField] private PlacedObject _placedObject;
    [SerializeField] private Vector3 _moveDirection;
    [SerializeField] private GridCell _currentGridCell;
    [SerializeField] private float _distanceTime;
    [SerializeField] private Vector3 _pointA;
    [SerializeField] private Vector3 _pointB;
    [SerializeField] private Vector3 _middlePoint;

    private void OnValidate()
    {
        _placedObject = GetComponent<PlacedObject>();
    }

    private void Update()
    {
        if (_currentGridCell == null) return;
        
        Move();
    }

    public void StartMove()
    {
        _currentGridCell = _placedObject.CurrentGridCell;
        
        UpdatePoints();
        SetPosition();
    }

    private void Move()
    {
        _distanceTime += Time.deltaTime * _moveSpeedMultiplier;
            
        SetPosition();
            
        if (_distanceTime >= 1)
        {
            _distanceTime = 0;
            
            // UPDATE CURRENTGRIDCELL VALUES
            _currentGridCell = _currentGridCell.GetNeighbourByDirection(_moveDirection);
            
            if (_currentGridCell == null)
            {
                Destroy(this.gameObject);
                GameManager.Instance.globalEventBus.triggerEvent("EnemyReachedEnd");
                Debug.Log("Enemy reached the end and is destroyed.");
                return;
            }
            ;
                
            UpdatePoints();
        }
    }

    private void SetPosition()
    {
        Vector3 evaluatedPointPosition = EvaluatePointPosition(_distanceTime);
        Vector3 evaluatedPointRotation = EvaluatePointPosition(_distanceTime + 0.001f);
        
        transform.position = new Vector3(evaluatedPointPosition.x, transform.position.y, evaluatedPointPosition.z);
        transform.LookAt(new Vector3(evaluatedPointRotation.x, transform.position.y, evaluatedPointRotation.z));
    }
    
    private Vector3 EvaluatePointPosition(float time)
    {
        Vector3 aToMiddle = Vector3.Lerp(_pointA, _middlePoint, time);
        Vector3 middleToB = Vector3.Lerp(_middlePoint, _pointB, time);
        
        return Vector3.Lerp(aToMiddle, middleToB, time);
    }
    
    private void UpdatePoints()
    {
        Vector3 directionA;
        Vector3 directionB;
        
        GridCell previousGridCell = _currentGridCell.GetNeighbourByDirection(-_moveDirection);
        GridCell nextGridCell = _currentGridCell.GetNeighbourByDirection(_moveDirection);
        
        if (previousGridCell != null)
            directionA = (previousGridCell.transform.position - _currentGridCell.transform.position).normalized;
        else
            directionA = Vector3.zero;
        
        if (nextGridCell != null)
            directionB = (nextGridCell.transform.position - _currentGridCell.transform.position).normalized;
        else
            directionB = Vector3.zero;

        _middlePoint = _currentGridCell.transform.position;
        _pointA = _currentGridCell.transform.position + directionA * 0.5f;
        _pointB = _currentGridCell.transform.position + directionB * 0.5f;
    }
}
