using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;

public class EnemyDefault : MonoBehaviour
{
    private BulletController _bulletController;
    private Rigidbody2D _rb2d;

    private void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _bulletController = FindObjectOfType<BulletController>();
        StartCoroutine(Shoot());
    }

    private void FixedUpdate()
    {   
        if (transform.position.x < -7)
            _rb2d.velocity = new Vector2(2, 0);
        if (transform.position.x > 7)
            _rb2d.velocity = new Vector2(-2, 0);
    }

    private IEnumerator Shoot()
    {
        yield return new WaitForSeconds(1);
        _bulletController.GenerateEnemyBullet(transform.position);
        StartCoroutine(Shoot());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.gameObject.layer == LayerMask.NameToLayer("Enemies"))
            return;

        Destroy(gameObject);
    }
}
