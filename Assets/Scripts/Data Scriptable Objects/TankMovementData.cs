using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewTankMovementData", menuName = "DataSO/TankMovementData")]
public class TankMovementData : ScriptableObject
{
    public float moveSpeed = 3f;
    public float rotationSpeed = 300f;
}
