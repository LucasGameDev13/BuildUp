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

    
    private void OnEnable()
    {
        if(GameController.Instance == null) return;
        GameController.Instance.OnReleasedCube -= TriggerReleaseCube;
        GameController.Instance.OnReleasedCube += TriggerReleaseCube;
    }

    private void OnDisable()
    {
        if (GameController.Instance == null) return;
        GameController.Instance.OnReleasedCube -= TriggerReleaseCube;
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

        if(currentCube != null)
        {
            currentCube.GetComponent<CubeController>().CubeSoundController.PlayCubeInstantiateSound();
        }
    }

    public void AddSpawnHeight(Vector3 offset)
    {
        spawnPoint.position += offset;
    }


    private void TriggerReleaseCube()
    {
        SpawnCube();

        if (currentCube != null)
        {
            currentCube.GetComponent<CubeController>().CubeSoundController.PlayCubeFallSound();
        }
    }
}
