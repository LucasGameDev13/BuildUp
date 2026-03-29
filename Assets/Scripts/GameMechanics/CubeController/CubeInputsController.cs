using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CubeInputsController : MonoBehaviour
{
    [Header("Movement Keys")]
    [SerializeField] private KeyCode rightMovementKey = KeyCode.D;
    [SerializeField] private KeyCode leftMovementKey = KeyCode.A;
    [SerializeField] private KeyCode forwardMovementKey = KeyCode.W;
    [SerializeField] private KeyCode backwardMovementKey = KeyCode.S;
    [SerializeField] private KeyCode releaseMovementKey = KeyCode.Space;
    private bool isLocked = true;


    private Vector3 movementInputs;

    public Vector3 MovementInputs()
    {
        if (isLocked)
        {
            if (Input.GetKey(rightMovementKey))
            {
                movementInputs = Vector3.right;
            }
            if (Input.GetKeyUp(rightMovementKey))
            {
                movementInputs = Vector3.zero;
            }

            if (Input.GetKey(leftMovementKey))
            {
                movementInputs = Vector3.left;
            }
            if (Input.GetKeyUp(leftMovementKey))
            {
                movementInputs = Vector3.zero;
            }

            if (Input.GetKey(forwardMovementKey))
            {
                movementInputs = Vector3.forward;
            }
            if (Input.GetKeyUp(forwardMovementKey))
            {
                movementInputs = Vector3.zero;
            }

            if (Input.GetKey(backwardMovementKey))
            {
                movementInputs = Vector3.back;
            }
            if (Input.GetKeyUp(backwardMovementKey))
            {
                movementInputs = Vector3.zero;
            }

        }

        return movementInputs;
    }

    public bool ReleaseMovementInput()
    {
        if(Input.GetKeyDown(releaseMovementKey))
        {
            movementInputs = Vector3.zero;
            GameController.Instance.SpawnerController.ReleaseCube();
            isLocked = false;
        }

        return isLocked;
    }
}
