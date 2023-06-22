using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private GameObject _bulletPrefab;
    [SerializeField]
    private GameObject _enemyBulletPrefab;

    public void GeneratePlayerBullet(Vector2 pos)
    {
        var bullet = Instantiate( _bulletPrefab );
        bullet.transform.position = pos;       

        var bulletRb2d = bullet.GetComponent<Rigidbody2D>();
        bulletRb2d.velocity = new Vector2(0, 5);

    }

    public void GenerateEnemyBullet(Vector2 pos)
    {
        var bullet = Instantiate(_enemyBulletPrefab);
        bullet.transform.position = pos;

        var bulletRb2d = bullet.GetComponent<Rigidbody2D>();
        bulletRb2d.velocity = new Vector2(0, -5);
    }
}
