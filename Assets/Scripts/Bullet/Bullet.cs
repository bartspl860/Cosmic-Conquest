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

    private Player _player;

    private void Start()
    {
        Bounds = new Vector2(
            Player._screenBounds.z, 
            Player._screenBounds.w
        );        
        _player = FindFirstObjectByType<Player>();
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
                    Destroy(collision.gameObject);
                    Destroy(gameObject);
                }
            break; 
            case ImpactOn.Player:
                if (collision.CompareTag(_impactTag.ToString()))
                {
                    Destroy(gameObject);
                    _player.TakeDamage();
                }
            break;
        }
    }
}
