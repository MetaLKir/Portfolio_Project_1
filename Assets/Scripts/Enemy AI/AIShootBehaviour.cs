using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShootBehaviour : AIBehaviour
{
    [SerializeField] float shootAngleFOV = 40f;

    public override void PerformAction(TankController tank, AIDetector detector)
    {
        if (TargetInFOV(tank, detector))
        {
            tank.HandleBodyMovement(Vector2.zero); // prevent from moving when shoot
            tank.HandleShoot();
        }
        tank.HandleTowerRotation(detector.Target.position);
    }

    bool TargetInFOV(TankController tank, AIDetector detector)
    {
        var direction = detector.Target.position - tank.tankTower.transform.position;
        if (Vector2.Angle(tank.tankTower.transform.up, direction) < shootAngleFOV / 2)
        {
            return true;
        }
        return false;
    }
}
