using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    [SerializeField] private int _positionX;
    [SerializeField] private int _positionY;
    [SerializeField] private List<GridCell> _gridCellNeighbours = new List<GridCell>();
    [SerializeField] private PlacedObject _currentPlacedObject;

    public int PositionX
    {
        get => _positionX;
        set => _positionX = value;
    }

    public int PositionY
    {
        get => _positionY;
        set => _positionY = value;
    }

    public List<GridCell> GridCellNeighbours
    {
        get => _gridCellNeighbours;
        set => _gridCellNeighbours = value;
    }
    
    public PlacedObject CurrentPlacedObject
    {
        get => _currentPlacedObject;
        set
        {
            _currentPlacedObject = value;
        }
    }

    public void Init(int positionX, int positionY)
    {
        _positionX = positionX;
        _positionY = positionY;
    }

    public GridCell GetNeighbourByDirection(Vector3 direction)
    {
        return _gridCellNeighbours.FirstOrDefault(item => (item.transform.position - transform.position).normalized == direction);
    }
}
