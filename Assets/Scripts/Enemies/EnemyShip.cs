using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;

namespace Enemies
{
    public class EnemyShip : Entity
    {
        private BulletController _bulletController;
        private Rigidbody2D _rb2d;
        private Coroutine _shootingCoroutine;
        
        private void Start()
        {
            _player = FindFirstObjectByType<Player.Player>();
            _rb2d = GetComponent<Rigidbody2D>();
            _bulletController = FindObjectOfType<BulletController>();
            _shootingCoroutine = StartCoroutine(Shoot());
            _utilitiesController = FindFirstObjectByType<UtilitiesController>();
            _utilitiesSpawnFunctions.Add(this._utilitiesController.SpawnBronzeShield);
            _utilitiesSpawnFunctions.Add(this._utilitiesController.SpawnBronzeAmmo);
            _utilitiesSpawnFunctions.Add(this._utilitiesController.SpawnHealth);
        }

        private void FixedUpdate()
        {   
            if (transform.position.x < -7)
                _rb2d.velocity = new Vector2(2, 0);
            if (transform.position.x > 7)
                _rb2d.velocity = new Vector2(-2, 0);
        }

        public void StopShooting()
        {
            StopCoroutine(_shootingCoroutine);
        }
        
        private IEnumerator Shoot()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                var rotation = transform.rotation.eulerAngles;
                rotation.z += 180f;
                _bulletController.GenerateEnemyBullet(transform.position, Quaternion.Euler(rotation));
            }
        }
    }
}
