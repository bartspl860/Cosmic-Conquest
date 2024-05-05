using System;
using System.Collections;
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

        protected void SpawnRandomUtility()
        {
            var chance = Random.Range(0f, 1f) <= 0.6f;
            if (chance)
            {
                Debug.Log(_utilitiesController);
                _utilitiesController.SpawnBronzeShield(transform.position);
            }
        }
        
        protected IEnumerator DestroySelf()
        {
            Destroy(GetComponent<Collider2D>());
            GetComponent<EnemyShip>().StopShooting();
            GetComponent<SpriteRenderer>().enabled = false;
            
            var ps = GetComponent<ParticleSystem>();
            ps.Play();
        
            AudioManager.Instance.PlaySound("asteroid_hit");
            
            SpawnRandomUtility();
            
            yield return new WaitForSeconds(ps.main.duration);

            Destroy(gameObject);
        }
    }
}