using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeVFXController : MonoBehaviour
{
    [Header("Cube Size Sorting Settings")]
    [SerializeField] private float maxXValue;
    [SerializeField] private float maxYValue;
    [SerializeField] private float maxZValue;

    [Header("Cube Feedback Direction Settings")]
    [SerializeField] private GameObject cubeLaserFeedbackVisual;

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

    public void TriggerVisualFeedBack(bool _value)
    {
        if(cubeLaserFeedbackVisual  != null)
        {
            cubeLaserFeedbackVisual.SetActive(_value);
        }
    }
}
