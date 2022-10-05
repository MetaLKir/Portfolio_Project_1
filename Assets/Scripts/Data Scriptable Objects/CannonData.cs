using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCannonData", menuName = "DataSO/CannonData")]
public class CannonData : ScriptableObject
{
    public GameObject bulletPrefab;
    public float reloadTime = 0.5f;
    public BulletData bulletData;
}
