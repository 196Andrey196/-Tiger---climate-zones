using System.Collections;
using UnityEngine;

public class DropSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _objectToSpawn;

    [SerializeField] private float _spawnRange;
    [SerializeField] private float _spawnInterval;
    private bool _firstSpawn = false;
    public void StartSpawn()
    {
        StartCoroutine(SpawnOjbect());
    }
    public void StopSpawn()
    {
        StopCoroutine(SpawnOjbect());
    }
    private IEnumerator SpawnOjbect()
    {
        if (!_firstSpawn)
        {
            yield return new WaitForSeconds(1f);
            _firstSpawn = true;
        }
        Vector3 spawnPosition = new Vector3(Random.Range(-_spawnRange, _spawnRange), transform.position.y - 0.5f, 0f);
        Instantiate(_objectToSpawn, spawnPosition, Quaternion.identity);
        yield return new WaitForSeconds(_spawnInterval);
        StartCoroutine(SpawnOjbect());
    }
}
