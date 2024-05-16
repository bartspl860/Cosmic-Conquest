using System;
using System.Collections;
using System.Collections.Generic;
using Audio;
using Controllers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
    public abstract class Entity : MonoBehaviour
    {
        protected Player.Player _player;
        protected UtilitiesController _utilitiesController;

        protected void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") && !_player.IsInvincible)
            {
                if (_player.IsShielded)
                {
                    _player.TriggerShield();
                }
                else
                {
                    _player.TakeDamage();
                    _player.TemporaryInvincibility(2.5f);
                }
            }
            else if (collision.CompareTag("PlayerBullet"))
            {
                StartCoroutine(DestroySelf());
            }
        }

        protected List<Action<Vector2>> _utilitiesSpawnFunctions = new List<Action<Vector2>>();
        protected void SpawnRandomUtility()
        {
            for (var i = 0; i < _utilitiesSpawnFunctions.Count; i++)
            {
                if (Random.Range(0f, 1f) <= 0.1f)
                {
                    _utilitiesSpawnFunctions[i](transform.position);
                    break;
                }
            }
            
        }
        protected IEnumerator DestroySelf()
        {
            Destroy(GetComponent<Collider2D>());
            if(TryGetComponent<EnemyShip>(out EnemyShip ship))
                ship.StopShooting();
            GetComponent<SpriteRenderer>().enabled = false;
            
            var ps = GetComponent<ParticleSystem>();
            ps.Play();
        
            AudioManager.Instance.PlaySound("asteroid_hit");
            
            SpawnRandomUtility();
            FindFirstObjectByType<EnviromentController>().EntityDestroyed();
            
            yield return new WaitForSeconds(ps.main.duration);
            
            Destroy(gameObject);
        }
    }
}