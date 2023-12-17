using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private enum ImpactOn
    {
        Destroyable,
        Player
    };
    private Vector2 Bounds { get; set; }
    [SerializeField]
    private ImpactOn _impactTag;

    private Player.Player _player;
    private ParticleSystem _particleSystem;

    private void Start()
    {
        Bounds = new Vector2(
            Player.Player._screenBounds.z, 
            Player.Player._screenBounds.w
        );        
        _player = FindFirstObjectByType<Player.Player>();
        _particleSystem = GetComponent<ParticleSystem>();
    }    

    private void Update()
    {
        var position = transform.position;

        if(position.y < Bounds.x - 10 || position.y > Bounds.y + 10)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(_impactTag)
        {
            case ImpactOn.Destroyable:                
                if (collision.CompareTag(_impactTag.ToString())){
                    StartCoroutine(DestroySelf());
                }
            break; 
            case ImpactOn.Player:
                if (collision.CompareTag(_impactTag.ToString()))
                {
                    StartCoroutine(DestroySelf());
                    if (!_player.IsShielded)
                    {
                        _player.TakeDamage();
                    }
                    else
                    {                        
                        _player.TriggerShield();
                    }
                }
            break;
        }
    }

    private IEnumerator DestroySelf()
    {
        _particleSystem.Play();
        var collider = GetComponent<Collider2D>();
        Destroy(collider);
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        yield return new WaitForSeconds(_particleSystem.main.duration);
        Destroy(gameObject);
    }
}
