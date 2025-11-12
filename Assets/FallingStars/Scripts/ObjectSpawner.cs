using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject fallingObjectPrefab;
    [SerializeField] private float spawnInterval = 1.5f;
    [SerializeField] private float spawnRangeX = 5f;

    private void Start()
    {
        if (fallingObjectPrefab == null)
        {
            Debug.LogError("FallingObjectPrefab is not assigned.");
            enabled = false;
            return;
        }

        InvokeRepeating(nameof(SpawnObject), spawnInterval, spawnInterval);
    }

    private void SpawnObject()
    {
        var xPosition = Random.Range(-spawnRangeX, spawnRangeX);
        var spawnPosition = new Vector3(xPosition, transform.position.y, 0f);

        Instantiate(fallingObjectPrefab, spawnPosition, Quaternion.identity);
    }
}
