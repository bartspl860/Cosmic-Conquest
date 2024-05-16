using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    private Player.Player _player;
    private void Start()
    {
        _player = FindFirstObjectByType<Player.Player>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _player.TakeDamage();
        }
    }
}
