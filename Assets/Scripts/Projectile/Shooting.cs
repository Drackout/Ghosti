using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private float cooldownTime;
    [SerializeField] private float projectileDamageMultiplier = 1.0f;
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform shootPoint;

    private float cooldownTimer = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldownTimer <= 0)
        {
            if (Input.GetButtonDown("Fire2"))
            {
                Projectile projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
                projectile.Damage = Mathf.FloorToInt(projectile.Damage * projectileDamageMultiplier);

                cooldownTimer = cooldownTime;
            }
        }
        else
        {
            cooldownTimer -= Time.deltaTime;
        }
    }
}
