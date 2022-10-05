using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDetector : MonoBehaviour
{
    [SerializeField] [Range(1, 15)] float viewRadius = 3;
    [SerializeField] float detectionCheckDelay = 0.25f;

    [SerializeField] Transform target = null;
    [SerializeField] LayerMask playerMask;
    [SerializeField] LayerMask visibilityLayer;

    [field: SerializeField] 
    public bool isTargetVisible { get; private set; }
    public Transform Target
    {
        get => target;
        set
        {
            target = value;
            isTargetVisible = false;
        }
    }

    private void Start()
    {
        StartCoroutine(DetectionCoroutine());
    }

    private void Update()
    {
        if (Target != null)
        {
            isTargetVisible = CheckIsTargetVisible();
        }
    }

    bool CheckIsTargetVisible()
    {
        var result = Physics2D.Raycast(transform.position, Target.position - transform.position, viewRadius, visibilityLayer);

        if (result.collider != null)
        {
            // Similar to Bit-operation "And" in assembler.
            // If "var result" is in the playerMask layer then "And" operation returns 1. Otherwise returns 0.
            // Then we compare "1 != 0" and get true (so, player visible), otherwise "0 != 0" get false (player behind object).
            return (playerMask & (1 << result.collider.gameObject.layer)) != 0;
        }
        return false;
    }

    void DetectTarget()
    {
        if (Target == null)
        {
            CheckIfPlayerInRange();
        }
        else if (Target != null)
        {
            CheckIsPlayerOutOfRange();
        }
    }

    void CheckIsPlayerOutOfRange()
    {
        if (Target.gameObject.activeSelf == false || Vector2.Distance(transform.position, Target.position) > viewRadius + 1) //
        {
            Target = null;
        }
        // viewRadius+1 prevents from situations when palyer's position is out of viewRadius but collider is in it,
        // so enemy lost and detect player again and again.
    }

    void CheckIfPlayerInRange()
    {
        Collider2D collision = Physics2D.OverlapCircle(transform.position, viewRadius, playerMask);

        if (collision != null)
        {
            Target = collision.transform;
        }
    }

    IEnumerator DetectionCoroutine()
    {
        yield return new WaitForSeconds(detectionCheckDelay);
        DetectTarget();
        StartCoroutine(DetectionCoroutine());
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, viewRadius);
    }
}
