using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovementController : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Settings")]
    [SerializeField] private float horizontalMovementSpeed = 5f;
    [SerializeField] private float gravityScale = -9.81f;
    private float verticalMovementSpeed = 1f;

    public Rigidbody Rb { get => rb; }

    private void Awake()
    {
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
        transform.position += new Vector3(x, 0, z) * Time.deltaTime;

    }

    public void UnlockGravityAction(bool _isLocked)
    {
        rb.isKinematic = _isLocked;
    }
}
