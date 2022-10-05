using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrolStaticBehaviour : AIBehaviour
{
    [SerializeField] float patrolCooldown = 2;
    float turretPatrolStep = 20;

    [SerializeField] Vector2 randomDirection = Vector2.zero;
    [SerializeField] float patrolCooldownCurrent;
    //----------------------------------------------//
    private void Awake()
    {
        randomDirection = Random.insideUnitCircle;
    }
    //----------------------------------------------//
    public override void PerformAction(TankController tank, AIDetector detector)
    {
        float angle = Vector2.Angle(tank.tankTower.transform.up, randomDirection);

        if (patrolCooldownCurrent <= 0 && (angle < turretPatrolStep))
        {
            randomDirection = Random.insideUnitCircle;
            patrolCooldownCurrent = patrolCooldown;
        }
        else
        {
            if (patrolCooldownCurrent > 0)
                patrolCooldownCurrent -= Time.deltaTime;
            else
            {
                tank.HandleTowerRotation((Vector2)tank.tankTower.transform.position + randomDirection);
            }
        }

    }
}
