using System.Collections;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private PlacedObject _placedObjectPrefab;
    [SerializeField] private GridCell[] _spawnPoints;

    public void SpawnEnemyWave()
    {
        StartCoroutine(SpawnEnemyWaveCoroutine());
    }

    private IEnumerator SpawnEnemyWaveCoroutine()
    {
        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            int isSpawned = Random.Range(0, 2);

            if (isSpawned == 1)
            {
                yield return new WaitForSeconds(i * 0.1f);
                
                PlacedObject placedObject = Instantiate(_placedObjectPrefab, _spawnPoints[i].transform.position + Vector3.up, Quaternion.identity);
                placedObject.CurrentGridCell = _spawnPoints[i];
                _spawnPoints[i].CurrentPlacedObject = placedObject;
                placedObject.GetComponent<SpawnAnimation>().Animate();
            }
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(EnemySpawner))]
public class EnemySpawnerEditor : Editor
{
    private EnemySpawner _enemySpawner;
    
    private void OnEnable()
    {
        _enemySpawner = (EnemySpawner)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        serializedObject.UpdateIfRequiredOrScript();
        
        GUI.color = Color.green;
        if (GUILayout.Button("Spawn Enemy Wave"))
            _enemySpawner.SpawnEnemyWave();

        serializedObject.ApplyModifiedProperties();
    }
}
#endif
