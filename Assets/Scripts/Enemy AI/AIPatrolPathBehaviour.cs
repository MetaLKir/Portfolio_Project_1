using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrolPathBehaviour : AIBehaviour
{
    [SerializeField] PatrolPath patrolPath;

    [Range(0.1f, 1f)] public float arriveDestance = 1f;
    [SerializeField] float waitTime = 0.5f;

    [SerializeField] Vector2 currentPatrolTarget = Vector2.zero;
    [SerializeField] bool isWaiting = false;
    bool isInitialized = false;

    int currentIndex = -1;

    //-----------------------------------------------------//

    private void Awake()
    {
        if (patrolPath == null)
        {
            patrolPath = GetComponentInChildren<PatrolPath>();
        }
    }

    //-----------------------------------------------------//
    public override void PerformAction(TankController tank, AIDetector detector)
    {
        if (!isWaiting)
        {
            if (patrolPath.Length < 2) // if has only one or no point in path
                return;

            if (!isInitialized)
            {
                var currentPathPoint = patrolPath.GetClosestPathPoint(tank.transform.position);
                currentIndex = currentPathPoint.Index;
                currentPatrolTarget = currentPathPoint.Position;
                isInitialized = true;
            }
            
            if (Vector2.Distance(tank.transform.position, currentPatrolTarget) < arriveDestance)
            {
                isWaiting = true;
                StartCoroutine(WaitCoroutine());
                return;
            }

            Vector2 directionToGo = currentPatrolTarget - (Vector2)tank.tankMovement.transform.position;
            tank.HandleBodyMovement(directionToGo.normalized);
        }

        IEnumerator WaitCoroutine()
        {
            yield return new WaitForSeconds(waitTime);
            var nextPathPoint = patrolPath.GetNextPathPoint(currentIndex);
            currentPatrolTarget = nextPathPoint.Position;
            currentIndex = nextPathPoint.Index;
            isWaiting = false;
        }
    }
}
