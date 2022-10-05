using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler sharedInstance;
    List<GameObject> pooledObjects;
    GameObject objectToPool;
    int amountToPool;

    private void Awake()
    {
        sharedInstance = this;
        pooledObjects = new List<GameObject>();
    }
    
    // Check pool list for a diactivated object and return first founded. Othervise return null.
    public GameObject GetPooledObject(List<GameObject> pool)
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                return pool[i];
            }
        }  
        return null;
    }

    public void CreatePool(List<GameObject> pool, GameObject objectToPool, int amountToPool)
    {
        // Create deactivated objects and set them inactive. 
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = (GameObject)Instantiate(objectToPool);
            obj.SetActive(false);
            pool.Add(obj);
            obj.transform.SetParent(this.transform); // attached to SpawnManager and set pool-objects as childs
        }
    }
}
