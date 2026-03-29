using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeVFXController : MonoBehaviour
{
    [SerializeField] private float maxXValue;
    [SerializeField] private float maxYValue;
    [SerializeField] private float maxZValue;
    private MeshRenderer mesh;

    private void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        InitializeScale();
        InitializeColor();
    }

    private void InitializeScale()
    {
        float randomX = Random.Range(1, maxXValue);
        float randomY = Random.Range(1, maxYValue);
        float randomZ = Random.Range(1, maxZValue);
        transform.localScale = new Vector3(randomX, randomY, randomZ);
    }

    private void InitializeColor()
    {

        if (mesh != null)
        {
            Color randomColor = new Color(Random.value, Random.value, Random.value);
            mesh.material.color = randomColor;
        }
    }
}
