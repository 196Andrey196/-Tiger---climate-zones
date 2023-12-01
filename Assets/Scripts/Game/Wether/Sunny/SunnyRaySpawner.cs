using System.Collections;
using UnityEngine;

public class SunnyRaySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _objectToSpawn;
    [SerializeField] private Collider2D _spawnZone;
    [SerializeField] private LayerMask _layersObjectsCannotSpawnOn;
    [SerializeField] private float _spawnInterval;
    [SerializeField] Collider2D[] colliders;
    public static int countSunnyRayPoint = 8;
    [SerializeField] int _currentCountPoints;
    private void Start()
    {
        _currentCountPoints = countSunnyRayPoint;
    }
    private IEnumerator SpawnObjects()
    {

        for (int i = 0; i < countSunnyRayPoint; i++)
        {
            Vector2 spawnPosition = GetRandomSpawnPosition(_spawnZone);
            Instantiate(_objectToSpawn, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(_spawnInterval);
        }

    }
    public void StartSpawn()
    {
        StartCoroutine(SpawnObjects());
    }

    public void StopSpawn()
    {
        StopCoroutine(SpawnObjects());
    }

    private Vector2 GetRandomSpawnPosition(Collider2D spawnableAreaCollider)
    {
        Vector2 spawPosition = Vector2.zero;
        bool isSpawnPosValid = false;
        int attempCount = 0;
        int maxAtemps = 200;

        while (!isSpawnPosValid && attempCount < maxAtemps)
        {
            spawPosition = GetRandomPointInCollider(spawnableAreaCollider);
            colliders = Physics2D.OverlapCircleAll(spawPosition, 2f);

            bool isInvalidCollision = false;
            foreach (Collider2D collider in colliders)
            {
                Debug.DrawRay(collider.bounds.center, Vector3.up * 2f, Color.red, 2f);
                if (collider.gameObject.layer != _layersObjectsCannotSpawnOn)
                {
                    isInvalidCollision = true;
                    break;
                }
            }
            if (!isInvalidCollision)
            {
                isSpawnPosValid = true;
            }
            attempCount++;
        }
        return spawPosition;
    }
    private Vector2 GetRandomPointInCollider(Collider2D collider, float offset = 1f)
    {
        Bounds colliderBounds = collider.bounds;
        Vector2 minBounds = new Vector2(colliderBounds.min.x + offset, colliderBounds.min.y + offset);
        Vector2 maxBounds = new Vector2(colliderBounds.max.x - offset, colliderBounds.max.y - offset);
        float randomX = Random.Range(minBounds.x, maxBounds.x);
        float randomY = Random.Range(minBounds.y, maxBounds.y);
        return new Vector2(randomX, randomY);
    }


}


