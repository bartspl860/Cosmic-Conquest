using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField, Header("Atributes")]
    private float _speed;
    [SerializeField]
    private int _lifes;
    [SerializeField]
    private int _points;
    [SerializeField, Header("If big asteroid, generate small")]    
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
            StartCoroutine(DestroySelf());
            var player = FindFirstObjectByType<Player>();

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
        //asteroid collides with bullet, takes damage and destroys itself immediately
        else
        {
            _lifes--;            
            if ( _lifes <= 0)
            {
                FindFirstObjectByType<Player>().AddScore(_points);
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
                Destroy(gameObject);
            }
                
        }
    }

    private IEnumerator DestroySelf()
    {
        var ps = GetComponent<ParticleSystem>();
        ps.Play();        
        GetComponent<SpriteRenderer>().enabled = false;
        
        
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
