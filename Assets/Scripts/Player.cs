using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed = 75.0f;
    [SerializeField] private float jumpSpeed = 100.0f;
    [SerializeField] private float jumpMaxTime = 0.1f;
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private int maxJumpCount = 1;

    //Colliders
    [SerializeField] private Collider2D groundCollider;
    [SerializeField] private Collider2D airCollider;

    //Probes
    [SerializeField] private Transform groundProbe;
    [SerializeField] private float groundProbeRadius = 1.0f;



    public Transform groundCheck;


    /* OLD DRAG
    [SerializeField] public float acceleration = 50.0f;
    [SerializeField] public float drag = 0.5f;
    */

    Rigidbody2D rb;
    Animator anim;
    private float jumpTime;
    int jumpsAvailable;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        jumpsAvailable = maxJumpCount;
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 currentVelocity = rb.velocity;

        // Movimento horizontal
        float hAxis = Input.GetAxis("Horizontal");


        /* OLD DRAG
        currentVelocity.x *= (1-drag);
        currentVelocity.x += hAxis * acceleration * Time.deltaTime;
        currentVelocity.x = Mathf.Clamp(currentVelocity.x, -horizontalSpeed, horizontalSpeed);
        */

        currentVelocity.x = hAxis * horizontalSpeed;

        //Detetar colisão por baixo do jogador
        //Collider2D groundCollision = Physics2D.OverlapCircle(groundCheck.position, 5, groundLayers);
        bool onGround = IsOnGround();

        

        groundCollider.enabled = onGround;
        airCollider.enabled = !onGround;

        // Saltar
        if ((Input.GetButtonDown("Jump")) && ((jumpsAvailable>0) || onGround))
        {
            currentVelocity.y = jumpSpeed;
            //Retirar a gravidade - para "saltar"
            rb.gravityScale = 0.0f;

            jumpTime = Time.deltaTime;
            jumpsAvailable--;
        }
        else if ((Input.GetButton("Jump")) && ((Time.time - jumpTime) < jumpMaxTime)){}
        
        else
        {
            rb.gravityScale = 5.0f;
        }



        // Set da velocidade
        rb.velocity = currentVelocity;

        // Animação
        anim.SetFloat("AbsVelX", Mathf.Abs(currentVelocity.x));

        if(currentVelocity.x < -0.5f)
        {
            if (transform.right.x > 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
        else if(currentVelocity.x > 0.5f)
        {
            if (transform.right.x < 0)
            {
                transform.rotation = Quaternion.identity;
            }
        }
    }

    private bool IsOnGround()
    {
        var collider = Physics2D.OverlapCircle(groundProbe.position, groundProbeRadius, groundLayers);
        return (collider != null);
    }

    void OnDrawGizmos()
    {
        if (groundProbe)
        {
            Gizmos.color = new Color(1.0f, 0.3f, 0.3f, 0.5f);
            Gizmos.DrawSphere(groundProbe.position, groundProbeRadius);
        }
    }
}
