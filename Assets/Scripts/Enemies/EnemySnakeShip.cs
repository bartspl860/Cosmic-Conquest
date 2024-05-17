using System;
using System.Collections.Generic;
using Controllers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemies
{
    public class EnemySnakeShip : Entity
    {
        private Rigidbody2D _rb2d;
        [SerializeField]
        private float _speed = 2;

        private float _verticalSpeed;
        private float _horizontalSpeed;
        
        public void Start()
        {
            _player = FindFirstObjectByType<Player.Player>();
            _rb2d = GetComponent<Rigidbody2D>();
            _utilitiesController = FindFirstObjectByType<UtilitiesController>();
            _utilitiesSpawnFunctions.Add(this._utilitiesController.SpawnBronzeShield);
            _utilitiesSpawnFunctions.Add(this._utilitiesController.SpawnBronzeAmmo);
            _utilitiesSpawnFunctions.Add(this._utilitiesController.SpawnHealth);
            _verticalSpeed = _speed;
            _horizontalSpeed = _speed;
        }
        
        private void FixedUpdate()
        {
            if (transform.position.x < -7)
                _horizontalSpeed = _speed;
            else if (transform.position.x > 7)
                _horizontalSpeed = -_speed;
            if (transform.position.y < -5)
                _verticalSpeed = _speed;
            else if (transform.position.y > 5)
                _verticalSpeed = -_speed;
            _rb2d.velocity = new Vector2(_horizontalSpeed, _verticalSpeed);
        }
    }
}