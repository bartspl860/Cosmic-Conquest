using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    [SerializeField]
    private GameObject _bulletPrefab;

    public void GenerateBullet(Vector2 pos)
    {
        var bullet = Instantiate( _bulletPrefab );
        bullet.transform.position = pos;

        var bulletRb2d = bullet.GetComponent<Rigidbody2D>();
        bulletRb2d.velocity = new Vector2(0, 5);
    }
}
