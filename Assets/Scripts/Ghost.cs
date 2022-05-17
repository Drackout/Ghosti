using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] private float speed = 60;
    [SerializeField] private Transform wallProbe;
    [SerializeField] private float probeRadius = 5;
    [SerializeField] private LayerMask probeMask;

    private Rigidbody2D rb;
    private float dirX = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector3 currentVelocity = rb.velocity;

        Collider2D collider = Physics2D.OverlapCircle(wallProbe.position, probeRadius, probeMask);
        if (collider != null)
        {
            currentVelocity = SwitchDirection(currentVelocity);
        }
        

        currentVelocity.x = speed * dirX;

        rb.velocity = currentVelocity;

        if ((currentVelocity.x > 0) && (transform.right.x < 0))
        {
            transform.rotation = Quaternion.identity;
        }
        else if ((currentVelocity.x < 0) && (transform.right.x > 0))
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private Vector3 SwitchDirection(Vector3 currentVelocity)
    {
        dirX = -dirX;

        if (currentVelocity.y > 0)
        {
            currentVelocity.y = 0;
        }

        return currentVelocity;
    }

    void OnDrawGizmos()
    {
        if (wallProbe)
        {
            Gizmos.color = new Color(1.0f, 0.0f, 1.0f, 0.5f);
            Gizmos.DrawSphere(wallProbe.position, probeRadius);
        }
    }

}
