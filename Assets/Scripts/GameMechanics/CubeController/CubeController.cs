using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    private CubeMovementController movementController;
    private CubeInputsController inputsController;
    private CubeVFXController cubeVFXController;
    private CubeSoundController cubeSoundController;
    private CubeCollisionController cubeCollisionController;

    private bool isConsiderableCube = false;

    public CubeMovementController MovementController { get => movementController; }
    public CubeSoundController CubeSoundController { get => cubeSoundController; }
    public bool IsConsiderableCube { get { return isConsiderableCube; } set { isConsiderableCube = value; } }

    private void Awake()
    {
        movementController = GetComponent<CubeMovementController>();
        inputsController = GetComponent<CubeInputsController>();
        cubeVFXController = GetComponent<CubeVFXController>();
        cubeSoundController = GetComponent<CubeSoundController>();
        cubeCollisionController = GetComponent<CubeCollisionController>();
    }

    private void Update()
    {
        movementController.Move(inputsController.MovementInputs());
        movementController.UnlockGravityAction(inputsController.ReleaseMovementInput());
        cubeVFXController.TriggerVisualFeedBack(movementController.Rb.isKinematic);
    }
}
