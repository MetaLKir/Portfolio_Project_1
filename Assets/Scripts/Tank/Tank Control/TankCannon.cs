using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TankCannon : MonoBehaviour
{
    [SerializeField] CannonData cannonData;
    float reloadTimeCurrent;
    bool canShoot = true;

    Collider2D[] tankColliders;

    [SerializeField] int bulletPoolSize = 6;
    List<GameObject> bulletPool = new List<GameObject>();

    //----------------------------------------------//
    public UnityEvent OnShoot, OnCantShoot;
    public UnityEvent<float> OnReload;
    //----------------------------------------------//
    void Awake()
    {
        tankColliders = GetComponentsInParent<Collider2D>();
        OnReload?.Invoke(reloadTimeCurrent);
    }

    void Start()
    {
        ObjectPooler.sharedInstance.CreatePool(bulletPool, cannonData.bulletPrefab, bulletPoolSize);
    }

    void Update()
    {
        CheckReload();
    }

    private void OnDestroy()
    {
        foreach (var bullet in bulletPool) // When cannon destroyed it's bullet pool destroyed too
        {
            Destroy(bullet);
        }
    }
    //----------------------------------------------//

    void CheckReload()
    {
        if (!canShoot)
        {
            reloadTimeCurrent -= Time.deltaTime;
            OnReload?.Invoke(reloadTimeCurrent / cannonData.reloadTime);
            if (reloadTimeCurrent <= 0)
            {
                canShoot = true;
            }
        }
    }

    public void Shoot()
    {
        if (canShoot)
        {
            canShoot = false;
            reloadTimeCurrent = cannonData.reloadTime;

            // Get an object object from the pool
            GameObject bullet = ObjectPooler.sharedInstance.GetPooledObject(bulletPool);
            if (bullet != null)
            {
                bullet.SetActive(true); // activate it

                bullet.transform.position = transform.GetChild(0).transform.position; // Take pos of shot point, otherwise bullets will fly from tank tower point.
                bullet.transform.localRotation = transform.rotation;
                bullet.GetComponent<Bullet>().Initialize(cannonData.bulletData);

                foreach (var collider in tankColliders) // prevents colliding with shooter itself
                {
                    Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), collider);
                }
            }
            OnShoot?.Invoke();
            OnReload?.Invoke(reloadTimeCurrent);
        }
        else OnCantShoot?.Invoke(); 
    }
}
