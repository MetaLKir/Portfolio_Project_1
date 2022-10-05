using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    public TankMovement tankMovement;
    public TankTower tankTower;
    public TankCannon[] tankCannons;

    void Awake()
    {
        if (tankMovement == null)
            tankMovement = GetComponentInChildren<TankMovement>();
        
        if (tankTower == null)
            tankTower = GetComponentInChildren<TankTower>();

        if (tankCannons == null || tankCannons.Length == 0)
            tankCannons = GetComponentsInChildren<TankCannon>();
    }

    //----------------------------------------------------------//
    public void HandleBodyMovement(Vector2 moveDirection)
    {
        tankMovement.SetMovementDirection(moveDirection);
    }    

    public void HandleTowerRotation(Vector2 mousePosition)
    {
        tankTower.RotateTowerTo(mousePosition);
    }

    public void HandleShoot()
    {
        foreach (var cannon in tankCannons) // if has multiple cannons each will shoot
        {
            cannon.Shoot();
        }
    }
}
