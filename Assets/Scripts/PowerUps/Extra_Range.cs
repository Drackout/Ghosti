using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extra_Range : MonoBehaviour
{
    [SerializeField] private GameObject effectPrefab;
    [SerializeField] private int extraRange = 15;

    void OnTriggerEnter2D(Collider2D collider)
    {
        var Cannon_Beam = collider.GetComponent<Cannon_Beam>();
        if (Cannon_Beam == null) return;

        Cannon_Beam.IncreaseRange(extraRange);

        if (effectPrefab != null)
        {
            Instantiate(effectPrefab, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }
}
