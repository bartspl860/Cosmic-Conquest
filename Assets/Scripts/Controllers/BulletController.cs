using System;
using Audio;
using UnityEngine;

namespace Controllers
{
    public class BulletController : MonoBehaviour
    {
        [SerializeField]
        private GameObject _bulletPrefab;
        [SerializeField]
        private GameObject _enemyBulletPrefab;

        [SerializeField, Header("Atributes")]
        private float _playerBulletSpeed;
        [SerializeField]
        private float _enemyBulletSpeed;

        public void GeneratePlayerBullet(Vector2 pos, Quaternion rot)
        {
            var bullet = Instantiate( _bulletPrefab );
            bullet.transform.position = pos;
            bullet.transform.rotation = rot;

            float angle = rot.eulerAngles.z * -Mathf.Deg2Rad; 

            var bulletRb2d = bullet.GetComponent<Rigidbody2D>();
            bulletRb2d.velocity = new Vector2((float)Math.Sin(angle), (float)Math.Cos(angle)) * _playerBulletSpeed;

            AudioManager.Instance.PlaySound("shoot");
        }

        public void GenerateEnemyBullet(Vector2 pos, Quaternion rot)
        {
            var bullet = Instantiate( _enemyBulletPrefab );
            bullet.transform.position = pos;
            bullet.transform.rotation = rot;

            float angle = rot.eulerAngles.z * -Mathf.Deg2Rad; 

            var bulletRb2d = bullet.GetComponent<Rigidbody2D>();
            bulletRb2d.velocity = new Vector2((float)Math.Sin(angle), (float)Math.Cos(angle)) * _enemyBulletSpeed;
        }
    }
}
