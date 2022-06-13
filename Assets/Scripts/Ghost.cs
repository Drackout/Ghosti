using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] private float speed = 100;
    [SerializeField] private Transform wallProbe;
    [SerializeField] private float probeRadius = 5;
    [SerializeField] private LayerMask probeMask;
    [SerializeField] private int damage = 1;
    [SerializeField] private int maxHealth = 1;
    [SerializeField] private GameObject deathEffectPrefab;
    [SerializeField] private Transform deathEffectSpawnPoint;
    [SerializeField] private LineRenderer StunnedGfx;
    [SerializeField] private bool moving = true;
    [SerializeField] private IntValue scoreValue;

    [SerializeField] private float amplitude = 10.0f;
    [SerializeField] private float frequency = 1.0f;

    private Rigidbody2D rb;
    private float dirX = 1;
    private int health;
    private float freezeTimer = 0.0f;
    private bool frozenThisFrame;
    private float thawTimer = 0.0f;
    private float deathAngle = 1;

    private Vector3 initialPosition;





    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = maxHealth;
        initialPosition = transform.position;
    }

    void Update()
    {
        Vector3 currentVelocity = rb.velocity;
        
        if (thawTimer > 0.0f)
        {
            currentVelocity = Vector3.up * 30 * Mathf.Sin(20 * 2 * Mathf.PI * Time.time);
            currentVelocity.x = 0;

            thawTimer -= Time.deltaTime;
        }
        else if (moving != true)
        {
            currentVelocity = Vector3.up * 5 * Mathf.Sin(1 * 2 * Mathf.PI * Time.time);
            currentVelocity.x = 0;
        }
        else
        {
            Collider2D collider = Physics2D.OverlapCircle(wallProbe.position, probeRadius, probeMask);
            if (collider != null)
            {
                currentVelocity = SwitchDirection(currentVelocity);
            }
            
            currentVelocity = Vector3.up * amplitude * Mathf.Sin(frequency * 2 * Mathf.PI * Time.time);
            currentVelocity.x = speed * dirX;
        }

        if ((currentVelocity.x > 0) && (transform.right.x < 0))
        {
            transform.rotation = Quaternion.identity;
        }
        else if ((currentVelocity.x < 0) && (transform.right.x > 0))
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        rb.velocity = currentVelocity;
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

    private void OnTriggerStay2D(Collider2D collider)
    {
        var player = collider.GetComponent<Player>();
        if (player != null)
        {
            Rigidbody2D playerRB = player.GetComponent<Rigidbody2D>();

            player.DealDamage(damage, transform);
            return;
        }
    }

    public void DealDamage(int damage)
    {
        if (thawTimer > 0)
        {
            health = health - damage;

            Debug.Log($"Ouch Enemy, health={health}");

            if (health <= 0)
            {
                Destroy(gameObject);

                if (deathEffectPrefab != null)
                {
                    Instantiate(deathEffectPrefab, deathEffectSpawnPoint.position, deathEffectSpawnPoint.rotation);
                }
                
                scoreValue.ChangeValue(1);
            }
            else
            {

            }
        }
    }

    void OnDrawGizmos()
    {
        if (wallProbe)
        {
            Gizmos.color = new Color(1.0f, 0.0f, 1.0f, 0.5f);
            Gizmos.DrawSphere(wallProbe.position, probeRadius);
        }
    }

    public void Freeze()
    {
        freezeTimer += Time.deltaTime;
        frozenThisFrame = true;

        if (freezeTimer > 2.0f)
        {
            thawTimer = 2.0f;
        }
    }

    private void LateUpdate()
    {
        if (!frozenThisFrame)
        {
            freezeTimer = 0;
        }

        if (StunnedGfx)
        {
            if ((freezeTimer > 0) || (thawTimer > 0))
            {
                StunnedGfx.enabled = true;

                var color = StunnedGfx.startColor;
                color.a = Mathf.Clamp01(Mathf.Max(freezeTimer, thawTimer) / 2.0f);
                StunnedGfx.startColor = color;
                color = StunnedGfx.endColor;
                color.a = Mathf.Clamp01(Mathf.Max(freezeTimer, thawTimer) / 2.0f);
                StunnedGfx.endColor = color;
            }
            else
            {
                StunnedGfx.enabled = false;
            }
        }

        frozenThisFrame = false;
    }
}