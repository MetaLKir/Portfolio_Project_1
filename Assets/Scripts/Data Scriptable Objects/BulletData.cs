using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBulletData", menuName = "DataSO/BulletData")]
public class BulletData : ScriptableObject
{
    public int damage = 40;
    public float speed = 5f;
    public float maxDistance = 5f;
}
