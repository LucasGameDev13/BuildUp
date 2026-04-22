using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovementController : MonoBehaviour
{
    private Rigidbody rb;
    private Transform myCameraPosition;

    [Header("Settings")]
    [SerializeField] private float horizontalMovementSpeed = 5f;
    [SerializeField] private float gravityScale = -9.81f;
    private float verticalMovementSpeed = 1f;
    private Bounds movementBounds;
    private bool hasBounds = false;

    public Rigidbody Rb { get => rb; }

    private void Awake()
    {
        myCameraPosition = Camera.main.transform;
        rb = GetComponent<Rigidbody>();
        UnlockGravityAction(true);
        SetGravity();
    }


    private void SetGravity()
    {
        Physics.gravity = new Vector3(0, gravityScale, 0);
    }

    public void Move(Vector3 _movementInput)
    {
        float x = _movementInput.x * horizontalMovementSpeed;

        float z = _movementInput.z * horizontalMovementSpeed;

        Vector3 camDir = myCameraPosition.TransformDirection(new Vector3(x, 0, z));

        camDir.y = 0;

        Vector3 newPos = transform.position + camDir * Time.deltaTime;

        if(hasBounds)
        {
            newPos.x = Mathf.Clamp(newPos.x, movementBounds.min.x + 1, movementBounds.max.x - 1);
            newPos.z = Mathf.Clamp(newPos.z, movementBounds.min.z + 1, movementBounds.max.z - 1);
        }

        transform.position = newPos;

    }

    public void SetMovementBounds(Bounds _bounds)
    {
        movementBounds = _bounds;
        hasBounds = true;
    }

    public void UnlockGravityAction(bool _isLocked)
    {
        rb.isKinematic = _isLocked;
    }
}
