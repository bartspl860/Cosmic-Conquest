using System.Collections;
using Controllers;
using UnityEngine;

namespace Enemies
{
    public class EnemyShip : MonoBehaviour
    {
        private BulletController _bulletController;
        private Rigidbody2D _rb2d;
        private Player.Player _player;
        
        private void Start()
        {
            _player = FindFirstObjectByType<Player.Player>();
            _rb2d = GetComponent<Rigidbody2D>();
            _bulletController = FindObjectOfType<BulletController>();
            StartCoroutine(Shoot());
        }

        private void FixedUpdate()
        {   
            if (transform.position.x < -7)
                _rb2d.velocity = new Vector2(2, 0);
            if (transform.position.x > 7)
                _rb2d.velocity = new Vector2(-2, 0);
        }

        private IEnumerator Shoot()
        {
            yield return new WaitForSeconds(1);
            _bulletController.GenerateEnemyBullet(transform.position);
            StartCoroutine(Shoot());
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                if (!_player.IsInvincible)
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
            }
        }
    }
}
