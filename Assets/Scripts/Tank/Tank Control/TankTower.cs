using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankTower : MonoBehaviour
{
    [SerializeField] float towerRotationSpeed = 300f;
    const float rotationOffset = 90f;

    public void RotateTowerTo(Vector2 pointPosition)
    {
        Vector2 lookDir = pointPosition - (Vector2)transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            Quaternion.Euler(0, 0, angle - rotationOffset), // 90f offset cause ration on x.axis, while cannon turned up (y.axis). 
            towerRotationSpeed * Time.deltaTime);
    }
}
