using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private PlacedObject _placedObjectPrefab;
    [SerializeField] private GridCell[] _spawnPoints;

    private void Start()
    {
        SpawnEnemyWave();
    }

    private void SpawnEnemyWave()
    {
        foreach (GridCell gridCell in _spawnPoints)
        {
            int isSpawned = Random.Range(0, 2);

            if (isSpawned == 1)
            {
                PlacedObject placedObject = Instantiate(_placedObjectPrefab, gridCell.transform.position + Vector3.up, Quaternion.identity);
                placedObject.CurrentGridCell = gridCell;
                gridCell.CurrentPlacedObject = placedObject;
            }
        }
    }
}
