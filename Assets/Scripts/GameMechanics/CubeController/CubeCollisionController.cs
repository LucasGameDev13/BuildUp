using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCollisionController : MonoBehaviour
{
    private CubeController cubeController;

    [SerializeField] private string baseInteraction;
    [SerializeField] private string cubeInteraction;

    private void Awake()
    {
        cubeController = GetComponent<CubeController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (IsLayer(collision.gameObject, baseInteraction) || IsLayer(collision.gameObject, cubeInteraction)) 
        { 
            cubeController.CubeSoundController.PlayCubeCollisionSound(); 
        }  
    }

    private bool IsLayer(GameObject obj, string layerName)
    {
        return obj.layer == LayerMask.NameToLayer(layerName);
    }
}
