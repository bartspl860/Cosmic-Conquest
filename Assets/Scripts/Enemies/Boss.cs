using System;
using System.Collections;
using Controllers;
using Effects;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Enemies
{
    public class Boss : MonoBehaviour
    {
        public enum Phase { Turrets, Minigun }
        
        [SerializeField]
        private int _health;

        [SerializeField] 
        private float _speed;
        
        private Rigidbody2D _bossRb2d;
        private Vector2 _flightDestination = Vector2.zero;
        
        private GameObject _playerGameObject;
        
        private BulletController _bulletController;

        [SerializeField] 
        private Hit _bossHitEffect;

        [SerializeField] 
        private Hit _shieldHitEffect;

        private Phase _phase = Phase.Turrets;

        private bool _shielded = true;

        private void Start()
        {
            _bossRb2d = GetComponent<Rigidbody2D>();
            _bulletController = FindFirstObjectByType<BulletController>();
            _playerGameObject = GameObject.FindWithTag("Player");
        }

        private void Move()
        {
            if (_flightDestination == Vector2.zero)
            {
                var posx = Random.Range(-8f, 8f);
                var posy = Random.Range(0f, 4f);
                _flightDestination = new Vector2(posx, posy);
            }
            
            var dir = _flightDestination - (Vector2)transform.position;
            _bossRb2d.velocity = dir.normalized * _speed;

            if (Vector2.Distance(transform.position, _flightDestination) < 1f)
            {
                _flightDestination = Vector2.zero;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("PlayerBullet"))
            {
                if (_shielded)
                {
                    StartCoroutine(_shieldHitEffect.StartEffect());
                }
                else
                {
                    StartCoroutine(_bossHitEffect.StartEffect());
                }
                
            }
        }

        private void Update()
        {
            Move();
            _shieldHitEffect.setVisible(_shielded);
        }
    }
}