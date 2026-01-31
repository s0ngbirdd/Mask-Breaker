using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class GridController : MonoBehaviour
{
    [Header("GRID OBJECTS")]
    [SerializeField] private GridCell _gridCellPrefab;
    [SerializeField] private List<GridCell> _spawnedGridCells = new List<GridCell>();
    
    [Header("GRID VALUES")]
    [SerializeField] int _width = 10;
    [SerializeField] int _height = 10;

    private Dictionary<string, GridCell> _gridCellDictionary = new Dictionary<string, GridCell>();
    
    public List<GridCell> SpawnedGridCells => _spawnedGridCells;

    private void Start()
    {
        foreach (GridCell gridCell in _spawnedGridCells)
            _gridCellDictionary[gridCell.PositionX + "" + gridCell.PositionY] = gridCell;
    }

    public GridCell GetGridCell(int x, int y)
    {
#if UNITY_EDITOR
        GridCell gridCell = _spawnedGridCells.FirstOrDefault(cell => cell.PositionX == x && cell.PositionY == y);
        return gridCell;
#else
        string key = x + "" + y;
        
        if (_gridCellDictionary.ContainsKey(key))
            return _gridCellDictionary[key];
        else
            return null;
#endif
    }
    
#if UNITY_EDITOR
    public void SpawnGrid()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                Vector3 position = new Vector3(x, y);

                GridCell gridCell = PrefabUtility.InstantiatePrefab(_gridCellPrefab) as GridCell;
                gridCell.transform.position = new Vector3(position.x, 0, position.y);
                gridCell.transform.rotation = transform.rotation;
                gridCell.transform.SetParent(transform);
                
                _spawnedGridCells.Add(gridCell);
                
                gridCell.Init(x, y);
            }
        }

        for (int i = 0; i < _spawnedGridCells.Count; i++)
            SetNeighbours(_spawnedGridCells[i], _spawnedGridCells[i].PositionX, _spawnedGridCells[i].PositionY);
    }
    
    public void DestroyGrid()
    {
        for (int i = 0; i < _spawnedGridCells.Count; i++)
            DestroyImmediate(_spawnedGridCells[i].gameObject);
        
        _spawnedGridCells.Clear();
    }
    
    private void SetNeighbours(GridCell gridCell, int x, int y)
    {
        if (x == 0 && y == 0)
        {
            gridCell.GridCellNeighbours.Add(_spawnedGridCells.FirstOrDefault(cell => cell.PositionX == x && cell.PositionY == y + 1)); // [x, y + 1]
            gridCell.GridCellNeighbours.Add(_spawnedGridCells.FirstOrDefault(cell => cell.PositionX == x + 1 && cell.PositionY == y)); // [x + 1, y]
            //gridCell.GridCellNeighbours.Add(_spawnedGridCells.FirstOrDefault(cell => cell.PositionX == x + 1 && cell.PositionY == y + 1)); // [x + 1, y + 1]
        }
        else if (x == 0 && y == _height - 1)
        {
            gridCell.GridCellNeighbours.Add(_spawnedGridCells.FirstOrDefault(cell => cell.PositionX == x && cell.PositionY == y - 1)); // [x, y - 1]
            gridCell.GridCellNeighbours.Add(_spawnedGridCells.FirstOrDefault(cell => cell.PositionX == x + 1 && cell.PositionY == y)); // [x + 1, y]
            //gridCell.GridCellNeighbours.Add(_spawnedGridCells.FirstOrDefault(cell => cell.PositionX == x + 1 && cell.PositionY == y - 1)); // [x + 1, y - 1]
        }
        else if (x == _width - 1 && y == _height - 1)
        {
            gridCell.GridCellNeighbours.Add(_spawnedGridCells.FirstOrDefault(cell => cell.PositionX == x && cell.PositionY == y - 1)); // [x, y - 1]
            gridCell.GridCellNeighbours.Add(_spawnedGridCells.FirstOrDefault(cell => cell.PositionX == x - 1 && cell.PositionY == y)); // [x - 1, y]
            //gridCell.GridCellNeighbours.Add(_spawnedGridCells.FirstOrDefault(cell => cell.PositionX == x - 1 && cell.PositionY == y - 1)); // [x - 1, y - 1]
        }
        else if (x == _width - 1 && y == 0)
        {
            gridCell.GridCellNeighbours.Add(_spawnedGridCells.FirstOrDefault(cell => cell.PositionX == x && cell.PositionY == y + 1)); // [x, y + 1]
            gridCell.GridCellNeighbours.Add(_spawnedGridCells.FirstOrDefault(cell => cell.PositionX == x - 1 && cell.PositionY == y)); // [x - 1, y]
            //gridCell.GridCellNeighbours.Add(_spawnedGridCells.FirstOrDefault(cell => cell.PositionX == x - 1 && cell.PositionY == y + 1)); // [x - 1, y + 1]
        }
        else if (x == 0 && y != 0 && y != _height - 1)
        {
            gridCell.GridCellNeighbours.Add(_spawnedGridCells.FirstOrDefault(cell => cell.PositionX == x && cell.PositionY == y + 1)); // [x, y + 1]
            gridCell.GridCellNeighbours.Add(_spawnedGridCells.FirstOrDefault(cell => cell.PositionX == x + 1 && cell.PositionY == y)); // [x + 1, y]
            gridCell.GridCellNeighbours.Add(_spawnedGridCells.FirstOrDefault(cell => cell.PositionX == x && cell.PositionY == y - 1)); // [x, y - 1]
            //gridCell.GridCellNeighbours.Add(_spawnedGridCells.FirstOrDefault(cell => cell.PositionX == x + 1 && cell.PositionY == y + 1)); // [x + 1, y + 1]
            //gridCell.GridCellNeighbours.Add(_spawnedGridCells.FirstOrDefault(cell => cell.PositionX == x + 1 && cell.PositionY == y - 1)); // [x + 1, y - 1]
        }
        else if (x != 0 && x != _width - 1 && y == _height - 1)
        {
            gridCell.GridCellNeighbours.Add(_spawnedGridCells.FirstOrDefault(cell => cell.PositionX == x - 1 && cell.PositionY == y)); // [x - 1, y]
            gridCell.GridCellNeighbours.Add(_spawnedGridCells.FirstOrDefault(cell => cell.PositionX == x && cell.PositionY == y - 1)); // [x, y - 1]
            gridCell.GridCellNeighbours.Add(_spawnedGridCells.FirstOrDefault(cell => cell.PositionX == x + 1 && cell.PositionY == y)); // [x + 1, y]
            //gridCell.GridCellNeighbours.Add(_spawnedGridCells.FirstOrDefault(cell => cell.PositionX == x - 1 && cell.PositionY == y - 1)); // [x - 1, y - 1]
            //gridCell.GridCellNeighbours.Add(_spawnedGridCells.FirstOrDefault(cell => cell.PositionX == x + 1 && cell.PositionY == y - 1)); // [x + 1, y - 1]
        }
        else if (x == _width - 1 && y != 0 && y != _height - 1)
        {
            gridCell.GridCellNeighbours.Add(_spawnedGridCells.FirstOrDefault(cell => cell.PositionX == x && cell.PositionY == y + 1)); // [x, y + 1]
            gridCell.GridCellNeighbours.Add(_spawnedGridCells.FirstOrDefault(cell => cell.PositionX == x - 1 && cell.PositionY == y)); // [x - 1, y]
            gridCell.GridCellNeighbours.Add(_spawnedGridCells.FirstOrDefault(cell => cell.PositionX == x && cell.PositionY == y - 1)); // [x, y - 1]
            //gridCell.GridCellNeighbours.Add(_spawnedGridCells.FirstOrDefault(cell => cell.PositionX == x - 1 && cell.PositionY == y + 1)); // [x - 1, y + 1]
            //gridCell.GridCellNeighbours.Add(_spawnedGridCells.FirstOrDefault(cell => cell.PositionX == x - 1 && cell.PositionY == y - 1)); // [x - 1, y - 1]
        }
        else if (x != 0 && x != _width - 1 && y == 0)
        {
            gridCell.GridCellNeighbours.Add(_spawnedGridCells.FirstOrDefault(cell => cell.PositionX == x + 1 && cell.PositionY == y)); // [x + 1, y]
            gridCell.GridCellNeighbours.Add(_spawnedGridCells.FirstOrDefault(cell => cell.PositionX == x && cell.PositionY == y + 1)); // [x, y + 1]
            gridCell.GridCellNeighbours.Add(_spawnedGridCells.FirstOrDefault(cell => cell.PositionX == x - 1 && cell.PositionY == y)); // [x - 1, y]
            //gridCell.GridCellNeighbours.Add(_spawnedGridCells.FirstOrDefault(cell => cell.PositionX == x + 1 && cell.PositionY == y + 1)); // [x + 1, y + 1]
            //gridCell.GridCellNeighbours.Add(_spawnedGridCells.FirstOrDefault(cell => cell.PositionX == x - 1 && cell.PositionY == y + 1)); // [x - 1, y + 1]
        }
        else
        {
            gridCell.GridCellNeighbours.Add(_spawnedGridCells.FirstOrDefault(cell => cell.PositionX == x && cell.PositionY == y + 1)); // [x, y + 1]
            gridCell.GridCellNeighbours.Add(_spawnedGridCells.FirstOrDefault(cell => cell.PositionX == x + 1 && cell.PositionY == y)); // [x + 1, y]
            gridCell.GridCellNeighbours.Add(_spawnedGridCells.FirstOrDefault(cell => cell.PositionX == x && cell.PositionY == y - 1)); // [x, y - 1]
            gridCell.GridCellNeighbours.Add(_spawnedGridCells.FirstOrDefault(cell => cell.PositionX == x - 1 && cell.PositionY == y)); // [x - 1, y]
            //gridCell.GridCellNeighbours.Add(_spawnedGridCells.FirstOrDefault(cell => cell.PositionX == x + 1 && cell.PositionY == y + 1)); // [x + 1, y + 1]
            //gridCell.GridCellNeighbours.Add(_spawnedGridCells.FirstOrDefault(cell => cell.PositionX == x + 1 && cell.PositionY == y - 1)); // [x + 1, y - 1]
            //gridCell.GridCellNeighbours.Add(_spawnedGridCells.FirstOrDefault(cell => cell.PositionX == x - 1 && cell.PositionY == y - 1)); // [x - 1, y - 1]
            //gridCell.GridCellNeighbours.Add(_spawnedGridCells.FirstOrDefault(cell => cell.PositionX == x - 1 && cell.PositionY == y + 1)); // [x - 1, y + 1]
        }
        
        gridCell.GridCellNeighbours.RemoveAll(item => item == null);
    }
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(GridController))]
public class GridControllerEditor : Editor
{
    private GridController _gridController;
    
    private void OnEnable()
    {
        _gridController = (GridController)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        serializedObject.UpdateIfRequiredOrScript();

        GUI.color = Color.green;
        if (GUILayout.Button("Spawn Grid"))
        {
            _gridController.SpawnGrid();
            
            EditorUtility.SetDirty(target);
        }

        GUI.color = Color.red;
        if (GUILayout.Button("Destroy Grid"))
        {
            _gridController.DestroyGrid();
            
            EditorUtility.SetDirty(target);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif
