using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extra_Range : MonoBehaviour
{
    [SerializeField] private GameObject effectPrefab;
    [SerializeField] private int extraRange = 15;

    void OnTriggerEnter2D(Collider2D collider)
    {
        var player = collider.GetComponent<Player>();
        if (player == null) return;

        var cannon = FindObjectOfType<Cannon_Beam>();
        if (cannon == null)return;

        cannon.IncreaseRange(extraRange);

        if (effectPrefab != null)
        {
            Instantiate(effectPrefab, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }
}
