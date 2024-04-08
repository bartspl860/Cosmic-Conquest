using System;
using System.Collections;
using Audio;
using UnityEngine;

namespace Enemies
{
    public abstract class Entity : MonoBehaviour
    {
        protected Player.Player _player;
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
        
        protected IEnumerator DestroySelf()
        {
            Destroy(GetComponent<Collider2D>());
            GetComponent<SpriteRenderer>().enabled = false;
        
            var ps = GetComponent<ParticleSystem>();
            ps.Play();
        
            AudioManager.Instance.PlaySound("asteroid_hit");
            
            yield return new WaitForSeconds(ps.main.duration);

            Destroy(gameObject);
        }
    }
}