using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCollisionController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        CubeController cubeController = other.GetComponent<CubeController>();

        if(cubeController == null) { return; }

        Invoke(nameof(GameOver), GameController.Instance.GetGameOverDelay());
    }

    private void GameOver()
    {
        GameController.Instance.ReleaseGameOverPanel();
    }
}
