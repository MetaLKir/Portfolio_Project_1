using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] BulletData bulletData;

    Vector2 startPos;
    float passedDistance = 0;

    //[SerializeField] int damage = 40;
    //[SerializeField] float speed = 10f;
    //[SerializeField] float maxDistance = 10f;

    public UnityEvent OnHit = new UnityEvent();

    //-------------------------------------------------//
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        CheckPassedDistance();
    }

    public void Initialize(BulletData bulletData)
    {
        this.bulletData = bulletData;
        
        startPos = transform.position;   
        rb.velocity = transform.up * bulletData.speed;
    }

    void CheckPassedDistance()
    {
        passedDistance = Vector2.Distance(transform.position, startPos);
        if (passedDistance >= bulletData.maxDistance)
        {
            DisableObject();
        }
    }

    void DisableObject()
    {
        rb.velocity = Vector2.zero;
        gameObject.SetActive(false);

        //Destroy(gameObject);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided with " + collision.name);

        OnHit?.Invoke();

        Hitpoints hitpoints = collision.GetComponent<Hitpoints>();
        if (hitpoints != null)
        {
            hitpoints.InflictDamage(bulletData.damage);
        }

        DisableObject();
    }
}
