using UnityEngine;

public class PlacedObject : MonoBehaviour
{
    [SerializeField] protected PlacedObjectType _placedObjectType;
    [SerializeField] protected GridCell _currentGridCell;

    public PlacedObjectType PlacedObjectType
    {
        get => _placedObjectType;
        set => _placedObjectType = value;
    }

    public GridCell CurrentGridCell
    {
        get => _currentGridCell;
        set => _currentGridCell = value;
    }
}
