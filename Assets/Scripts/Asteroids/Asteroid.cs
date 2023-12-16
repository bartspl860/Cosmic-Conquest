using System.Collections;
using System.Collections.Generic;
using Audio;
using UnityEngine;

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

    private bool _bigAsteroid;

    private float _rotation = 0f;
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //asteroid collides with player and makes particle effect
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(DestroySelf(collisionWithPlayer: true));
            var player = FindFirstObjectByType<Player>();

            if (player == null)
                return;

            if(!player.IsShielded)
            {
                player.TakeDamage();
            }
            else
            {
                player.TriggerShield();
            }
        }
        //asteroid checks if it collides with enemies and ignores it
        else if(collision.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            return;
        }
        //asteroid collides with bullet, takes damage and destroys itself when lives are 0
        else
        {
            _lifes--;            
            if ( _lifes <= 0)
            {
                var player = FindFirstObjectByType<Player>();
                if(player != null)
                {
                    player.AddScore(_points);
                }                
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
        var ps = GetComponent<ParticleSystem>();
        ps.Play();        
        GetComponent<SpriteRenderer>().enabled = false;
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
            Destroy(gameObject);
    }

    public int GetPoints()
    {
        return _points;
    }
}
