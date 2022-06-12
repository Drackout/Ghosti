using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 100.0f;
    [SerializeField] float projectileMaxRange = 2.0f;
    [SerializeField] float projectileProgress = 0f;
    [SerializeField] float timeDestroy = 4.0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (projectileProgress < projectileMaxRange)
        {
            transform.position = transform.position + transform.right * speed * Time.deltaTime;

            projectileProgress += Time.deltaTime;
        }
        else if (projectileProgress >= timeDestroy)
        {
            Destroy(gameObject);
        }

    }
}
