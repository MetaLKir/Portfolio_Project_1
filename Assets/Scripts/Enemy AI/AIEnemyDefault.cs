using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEnemyDefault : MonoBehaviour
{
    [SerializeField] AIBehaviour behaviourShoot, behaviorPatrol;

    [SerializeField] TankController tank;
    [SerializeField] AIDetector detector;

    private void Awake()
    {
        tank = GetComponentInChildren<TankController>();
        detector = GetComponentInChildren<AIDetector>();
    }

    private void Update()
    {
        if (detector.isTargetVisible)
        {
            behaviourShoot.PerformAction(tank, detector);
        }
        else
        {
            behaviorPatrol.PerformAction(tank, detector);
        }
    }
}
