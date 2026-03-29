using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    private CubeMovementController movementController;
    private CubeInputsController inputsController;

    private bool isConsiderableCube = false;

    public CubeMovementController MovementController { get => movementController; }
    public bool IsConsiderableCube { get { return isConsiderableCube; } set { isConsiderableCube = value; } }

    private void Awake()
    {
        movementController = GetComponent<CubeMovementController>();
        inputsController = GetComponent<CubeInputsController>();
    }

    private void Update()
    {
        movementController.Move(inputsController.MovementInputs());

        movementController.UnlockGravityAction(inputsController.ReleaseMovementInput());
    }
}
