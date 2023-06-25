using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private int _lifes;

    private float _rotation = 0f;
    private void Start()
    {
        var rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2 (0, -_speed);
        transform.rotation = 
            Quaternion.Euler(
                new Vector3(0f, 0f, Random.Range(0f, 180f))
            );
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(PlayerCollision());
            
        }
        else
        {
            _lifes--;
            if( _lifes <= 0 )
                Destroy(gameObject);
        }
    }

    private IEnumerator PlayerCollision()
    {
        var ps = GetComponent<ParticleSystem>();
        ps.Play();        
        GetComponent<SpriteRenderer>().enabled = false;
        FindFirstObjectByType<Player>().TakeDamage();
        
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
}
