using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TankMovement : MonoBehaviour
{
    Rigidbody2D rb; 
    Vector2 moveDirection;

    [SerializeField] TankMovementData movementData;

    public UnityEvent<float> OnSpeedChange = new UnityEvent<float>();

    void Awake()
    {
        rb = GetComponentInParent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        MovePlayer();
        RotatePlayer();
    }

    public void SetMovementDirection(Vector2 movementDirection)
    {
        this.moveDirection = movementDirection;
    }

    void MovePlayer()
    {
        rb.MovePosition(rb.position + moveDirection * movementData.moveSpeed * Time.fixedDeltaTime);
        OnSpeedChange?.Invoke(moveDirection.magnitude);
    }

    void RotatePlayer()
    {
        if (moveDirection != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, moveDirection);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                toRotation,
                movementData.rotationSpeed * Time.fixedDeltaTime);
        }
    }
}
