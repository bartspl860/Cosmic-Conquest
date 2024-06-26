using System;
using Player;
using System.Collections;
using Audio;
using Controllers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

namespace Asteroids
{
    public class Asteroid : MonoBehaviour
    {
        [SerializeField, Header("Atributes")]
        private float _speed;
        [SerializeField]
        private int _lifes;
        [SerializeField]
        private int _points;
        [SerializeField, Header("If big asteroid gets destroyed, generate small")]    
        private bool _isBrown;
        [SerializeField]
        private GameObject _smallBrownAsteroidPrefab;
        [SerializeField]
        private GameObject _smallSilverAsteroidPrefab;
        private Player.Player player;
        private bool _bigAsteroid;
        private float _rotation = 0f;
        private bool _destroyed = false;
        private void Start()
        {
            var rb2d = GetComponent<Rigidbody2D>();
            rb2d.velocity = new Vector2 (0, -_speed);
            transform.rotation = 
                Quaternion.Euler(
                    new Vector3(0f, 0f, Random.Range(0f, 180f))
                );
            if(_lifes > 1)
                _bigAsteroid = true;
            this.player = FindFirstObjectByType<Player.Player>();
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                if (!player.IsInvincible)
                {
                    if (player.IsShielded)
                    {
                        player.TriggerShield();
                    }
                    else
                    {
                        
                        player.TakeDamage();
                        player.TemporaryInvincibility(2.5f);
                    }

                    _destroyed = true;
                    StartCoroutine(DestroySelf(collisionWithPlayer: true));
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(_destroyed)
                return;
            //asteroid checks if it collides with enemies and ignores it
            if(collision.gameObject.layer == LayerMask.NameToLayer("Enemies"))
                return;
            //asteroid collides with player and makes particle effect
            if (collision.CompareTag("Player"))
            {
                if(!player.IsInvincible)
                    StartCoroutine(DestroySelf(collisionWithPlayer: true));

                if(player.IsShielded)
                {
                    player.TriggerShield();
                }
                else
                {
                    player.TakeDamage();
                    player.TemporaryInvincibility(2.5f);
                }
            }
            //asteroid collides with bullet, takes damage and destroys itself when lives are 0
            else
            {
                _lifes--;            
                if ( _lifes <= 0)
                {
                    player.AddScore(_points);               
                    if (_bigAsteroid)
                    {
                        var randomAsteroidNum = Random.Range(0, 3);
                        for(var i = 0; i < randomAsteroidNum; i++)
                        {
                            var position = (transform.position + Random.insideUnitSphere * Random.Range(0.0f, 2.0f));
                            GameObject prefab;
                            if (_isBrown)
                            {
                                prefab = Instantiate(_smallBrownAsteroidPrefab);
                            }
                            else
                            {
                                prefab = Instantiate(_smallSilverAsteroidPrefab);
                            }
                            prefab.transform.position = position;
                        }
                    }
                    StartCoroutine(DestroySelf());
                }
                else
                {
                    AudioManager.Instance.PlaySound("asteroid_hit");
                }
            }
        }

        private IEnumerator DestroySelf(bool collisionWithPlayer = false)
        {
            Destroy(GetComponent<Collider2D>());
            GetComponent<SpriteRenderer>().enabled = false;
        
            var ps = GetComponent<ParticleSystem>();
            ps.Play();
        
            if (collisionWithPlayer)
                AudioManager.Instance.PlaySound("hit");
            else
                AudioManager.Instance.PlaySound("asteroid_destroy");
            
            yield return new WaitForSeconds(ps.main.duration);
            
            Destroy(gameObject);
        }

        private void FixedUpdate()
        {
            transform.Rotate(new Vector3(0f, 0f, _rotation));
            _rotation += 0.001f;
            if (transform.position.y < -10)
            {
                Destroy(gameObject);
            }
        }

        public int GetPoints()
        {
            return _points;
        }
    }
}
