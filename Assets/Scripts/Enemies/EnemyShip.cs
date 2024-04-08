using System.Collections;
using Controllers;
using UnityEngine;

namespace Enemies
{
    public class EnemyShip : Entity
    {
        private BulletController _bulletController;
        private Rigidbody2D _rb2d;
        
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
    }
}
