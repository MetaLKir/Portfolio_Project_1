using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnerRandomlyInCircle : MonoBehaviour
{
    public GameObject prefabToSpawn;
    [SerializeField] float radius = 0.25f;
    [SerializeField] Color gizmosColor = Color.red;

    protected Vector2 GetRandomPosition()
    {
        return Random.insideUnitCircle * radius + (Vector2)transform.position;
    }

    protected Quaternion GetRandomRotation()
    {
        return Quaternion.Euler(0, 0, Random.Range(0, 360));
    }

    protected virtual GameObject GetObject()
    {
        return Instantiate(prefabToSpawn);
    }
    
    public void CreateObject()
    {
        Vector2 position = GetRandomPosition();
        GameObject impactObject = GetObject();
        impactObject.transform.position = position;
        impactObject.transform.rotation = GetRandomRotation();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmosColor;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
