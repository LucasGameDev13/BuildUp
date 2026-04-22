using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeLaserVFXCollisionController : MonoBehaviour
{
    private CubeLaserVFXController cubeLaserController;
    [SerializeField] private string baseInteraction;
    [SerializeField] private string lowBaseInteraction;

    [Header("Laser Height Settings")]
    [SerializeField] private float growthSpeed = 0.5f;

    private bool hasActivedTheLowBase;

    void Awake()
    {
        cubeLaserController = GetComponent<CubeLaserVFXController>();
    }

    void Update()
    {
        SetLaserHeight();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (IsLayer(other.gameObject, baseInteraction)) { cubeLaserController.SetMesh(true); }
        if (IsLayer(other.gameObject, lowBaseInteraction)) { hasActivedTheLowBase = true; }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsLayer(other.gameObject, baseInteraction)) { cubeLaserController.SetMesh(false); }
        if (IsLayer(other.gameObject, lowBaseInteraction)) { hasActivedTheLowBase = false; }
    }

    private bool IsLayer(GameObject obj, string layerName)
    {
        return obj.layer == LayerMask.NameToLayer(layerName);
    }
    private void SetLaserHeight()
    {
        if (hasActivedTheLowBase)
        {
            return;
        }

        cubeLaserController.SetLaserHeight(growthSpeed * Time.deltaTime);
    }
}
