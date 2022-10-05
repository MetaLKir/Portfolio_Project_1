using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trackmark : MonoBehaviour
{ 
    SpriteRenderer spriteRenderer;
    
    [SerializeField] float lifeTime = 5f;
    float lifeTimeCurrent;

    float fadeDelay = 0.1f;

    public bool isReadyBeDestroyed = false;

    //----------------------------------------------//

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        lifeTimeCurrent = lifeTime;
        StartCoroutine(TransparencyFade());
    }

    private void Update()
    {
        LifetimeFade();
    }

    //----------------------------------------------//
    void LifetimeFade()
    {
        if (gameObject.activeSelf)
        {
            if (lifeTimeCurrent > 0)
                lifeTimeCurrent -= Time.deltaTime;
            else
            {
                if (!isReadyBeDestroyed)
                    gameObject.SetActive(false);
                else Destroy(gameObject);
            }
        }
    }

    IEnumerator TransparencyFade()
    {
        for (float transparency = 1f; transparency > 0; transparency -= fadeDelay/lifeTime)
        {
            Color color = spriteRenderer.color;
            color.a = transparency;
            spriteRenderer.color = color;
            yield return new WaitForSeconds(fadeDelay);
        }
    }
}
