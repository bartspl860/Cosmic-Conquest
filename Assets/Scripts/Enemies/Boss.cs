using System;
using System.Collections;
using System.Collections.Generic;
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
        public enum Phase { Turrets, Laser }
        
        [SerializeField]
        private int _health;

        private int _maxHealth;

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

        [SerializeField]
        private BossTurret[] _turrets;

        [SerializeField] 
        private SpriteRenderer _laser;

        [SerializeField] private Healthbar _healthbar;
        [SerializeField] private ParticleSystem _particleSystem;

        private void Start()
        {
            _bossRb2d = GetComponent<Rigidbody2D>();
            _bulletController = FindFirstObjectByType<BulletController>();
            _playerGameObject = GameObject.FindWithTag("Player");
            _maxHealth = _health;
            _healthbar.SetValue(0f);
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
            _bossRb2d.velocity = dir.normalized * (_phase == Phase.Turrets ? _speed : _speed * 1.3f);

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
                    _health--;
                    _healthbar.SetValue(_health / (float)_maxHealth);

                    if (_health == 0)
                    {
                        StartCoroutine(DestroySelf());
                    }
                }
                
            }
        }

        private IEnumerator DestroySelf()
        {
            _particleSystem.Play();
            yield return new WaitForSeconds(_particleSystem.main.duration);
            Destroy(gameObject);
        }

        private IEnumerator Shoot()
        {
            while (true)
            {
                while (_laser.size.y < 30)
                {
                    _laser.size = new Vector2(_laser.size.x, _laser.size.y + 0.2f);
                    yield return null;
                }

                yield return new WaitForSeconds(2);
                _laser.size = new Vector2(_laser.size.x, 0);

                yield return new WaitForSeconds(4);
            }
        }

        private Coroutine _shooting;
        private void Update()
        {
            if (_health <= 0)
                return;
            
            Move();
            _shieldHitEffect.setVisible(_shielded);

            bool allTurretsDestroyed = true;
            foreach (var turret in _turrets)
            {
                if (turret.GetHealth() > 0)
                    allTurretsDestroyed = false;
            }

            if (allTurretsDestroyed)
            {
                if (_shooting == null)
                {
                    _phase = Phase.Laser;
                    _shielded = false;
                    _healthbar.SetValue(1f);
                    _shooting = StartCoroutine(Shoot());
                }
            }
        }
    }
}