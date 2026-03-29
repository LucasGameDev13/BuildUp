using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    [Header("Cube Prefab")]
    [SerializeField] private GameObject cubePrefab;

    [Header("Spawn Settings")]
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private Transform spawnPoint;


    private GameObject currentCube;

    private Coroutine spawnCo;

    public event Action OnCubeReleased;

    private void OnEnable()
    {
        OnCubeReleased -= SpawnCube;
        OnCubeReleased += SpawnCube;
    }

    private void OnDisable()
    {
        OnCubeReleased -= SpawnCube;
    }

    private void Start()
    {
        SpawnCube();
    }

    public void ReleaseCube()
    {
        OnCubeReleased?.Invoke();
    }

    private void SpawnCube()
    {
        if(spawnCo != null)
        {
            StopCoroutine(spawnCo);
        }

        spawnCo = StartCoroutine(SpawnTimer());
    }

    private IEnumerator SpawnTimer()
    {
        yield return new WaitForSeconds(spawnInterval);

        currentCube = Instantiate(cubePrefab, spawnPoint.transform.position, Quaternion.identity);
    }

    public void AddSpawnHeight(Vector3 offset)
    {
        spawnPoint.position += offset;
    }
}
