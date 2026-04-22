using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnerHighController : MonoBehaviour
{
    private CubeController cubeToAnalyse;
    private BoxCollider myColliderLimits;
    [SerializeField]private SpawnerController spawnerController;

    [Header("Spawner Settings")]
    [SerializeField] private Vector3 spawnOffSet;
    [SerializeField] private float checkInterval = 0.5f;

    private float timer = 0f;

    private void Awake()
    {
        if(spawnerController == null)
            spawnerController = GetComponent<SpawnerController>();

        myColliderLimits = GetComponent<BoxCollider>();
    }

    private void OnTriggerStay(Collider other)
    {
        cubeToAnalyse = other.GetComponent<CubeController>();

        if (cubeToAnalyse == null) return;

        if (cubeToAnalyse.MovementController == null) return;

        if (cubeToAnalyse.MovementController.Rb == null) return;

        Rigidbody cubeRb = cubeToAnalyse.MovementController.Rb;

        if (cubeRb.isKinematic && !cubeToAnalyse.IsConsiderableCube) { return; }

        cubeToAnalyse.MovementController.SetMovementBounds(myColliderLimits.bounds);

        float velocityY = Mathf.Abs(cubeRb.velocity.y);

        if (velocityY > 0.1f) return;

        timer += Time.deltaTime;

        if (timer >= checkInterval)
        {
            spawnerController.AddSpawnHeight(spawnOffSet);

            timer = 0f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CubeController cube = other.GetComponent<CubeController>();

        if (cube != null)
        {
            cube.IsConsiderableCube = true;
        }
    }
}
