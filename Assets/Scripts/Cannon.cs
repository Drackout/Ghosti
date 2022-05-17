using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public Transform shootPoint;
    public GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 150.0f;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // Converter a posição do rato para todas as cameras
        Vector2 mpPixelCoords = Input.mousePosition;
        Vector3 mpWorldCoords = mainCamera.ScreenToWorldPoint(mpPixelCoords);

        Vector2 toCursor = mpWorldCoords - transform.position;
        transform.right = toCursor;


        /* Se passar para trás do personagem
        if (transform.up.y < 0)
        {
            transform.up = -transform.up;
        }
        */

        // Fire1
        if (Input.GetButtonDown("Fire1"))
        {
            //Start firing the laser.. it going to mouse position
            FireLaser();
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            //Stop firing the laser.. it coming back
            Debug.Log("Laser come");
        }

    }

    private void FireLaser()
    {
        Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);

        projectilePrefab.transform.position = projectilePrefab.transform.position + projectilePrefab.transform.right * projectileSpeed * Time.deltaTime;

        
    }
}
