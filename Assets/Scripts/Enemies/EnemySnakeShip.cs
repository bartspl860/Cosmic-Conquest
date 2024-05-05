using System;
using UnityEngine;

namespace Enemies
{
    public class EnemySnakeShip : Entity
    {
        private Rigidbody2D _rb2d;
        private const float SPEED = 2;
        private float _verticalSpeed = SPEED;
        private float _horizontalSpeed = SPEED;

        
        
        public void Start()
        {
            _player = FindFirstObjectByType<Player.Player>();
            _rb2d = GetComponent<Rigidbody2D>();
        }
        
        private void FixedUpdate()
        {
            if (transform.position.x < -7)
                _horizontalSpeed = SPEED;
            else if (transform.position.x > 7)
                _horizontalSpeed = -SPEED;
            if (transform.position.y < -5)
                _verticalSpeed = SPEED;
            else if (transform.position.y > 5)
                _verticalSpeed = -SPEED;
            _rb2d.velocity = new Vector2(_horizontalSpeed, _verticalSpeed);
        }
    }
}