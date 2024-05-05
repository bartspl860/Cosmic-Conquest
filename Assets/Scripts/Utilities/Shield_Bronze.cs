using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class Shield_Bronze : Utility
{
    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -1f);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindFirstObjectByType<Player.Player>().ActivateShield();
            Destroy(gameObject);
        }
    }
}
