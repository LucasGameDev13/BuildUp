using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CubeInputsController : MonoBehaviour
{
    private bool isLocked = true;
    private InputsSystemController inputSystemActions;
    private Vector3 movementInputs;

    private void Awake()
    {
        inputSystemActions = new InputsSystemController();
    }

    private void OnEnable()
    {
        inputSystemActions.Enable();
    }

    private void OnDisable()
    {
        inputSystemActions.Disable();
    }

    public Vector3 MovementInputs()
    {
        if (isLocked)
        {
            Vector2 rawInput = inputSystemActions.Player.MoveCube.ReadValue<Vector2>();

            movementInputs = new Vector3(rawInput.x, 0f, rawInput.y);
        }

        return movementInputs;
    }

    public bool ReleaseMovementInput()
    {
        if (inputSystemActions.Player.ReleaseCube.WasPressedThisFrame() && isLocked)
        {
            movementInputs = Vector3.zero;
            GameController.Instance.ReleaseCubeEvents();
            isLocked = false;
        }


        return isLocked;
    }
}
