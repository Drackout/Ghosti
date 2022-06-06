using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extra_Jump : MonoBehaviour
{
    [SerializeField] private GameObject effectPrefab;
    [SerializeField] private int extraJumps=1;

    void OnTriggerEnter2D(Collider2D collider)
    {
        var player = collider.GetComponent<Player>();
        if (player == null) return;

        player.IncreaseJumps(extraJumps);

        if (effectPrefab != null)
        {
            Instantiate(effectPrefab, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }
}
