using System;
using UnityEngine;

namespace Enemies
{
    public class EnemySnakeShip : MonoBehaviour
    {
        private Rigidbody2D _rb2d;

        public void Start()
        {
            _rb2d = GetComponent<Rigidbody2D>();
        }
    }
}