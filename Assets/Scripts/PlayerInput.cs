using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] Camera cam;

    public UnityEvent Shooting = new UnityEvent();
    public UnityEvent<Vector2> TankMoving = new UnityEvent<Vector2>();
    public UnityEvent<Vector2> TowerRotating = new UnityEvent<Vector2>();

    //--------------------------------------------------//
    private void Awake()
    {
        if (cam == null)
        {
            cam = Camera.current;
        }
    }

    void Update()
    {
        GetBodyMovement();
        GetTowerRotation();
        GetShootingInput();
    }
    //--------------------------------------------------//
    void GetBodyMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 moveDirection = new Vector2(horizontal, vertical).normalized;

        TankMoving?.Invoke(moveDirection);
    }

    void GetTowerRotation()
    {
        TowerRotating?.Invoke(GetMousePos());
    }

    void GetShootingInput()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shooting?.Invoke();
        }
    }
    //--------------------------------------------------//
    Vector2 GetMousePos()
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        return mousePos;
    }
}
