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

        public void GeneratePlayerBullet(Vector2 pos)
        {
            var bullet = Instantiate( _bulletPrefab );
            bullet.transform.position = pos;       

            var bulletRb2d = bullet.GetComponent<Rigidbody2D>();
            bulletRb2d.velocity = new Vector2(0, _playerBulletSpeed);

            AudioManager.Instance.PlaySound("shoot");
        }

        public void GenerateEnemyBullet(Vector2 pos)
        {
            var bullet = Instantiate(_enemyBulletPrefab);
            bullet.transform.position = pos;

            var bulletRb2d = bullet.GetComponent<Rigidbody2D>();
            bulletRb2d.velocity = new Vector2(0, -_enemyBulletSpeed);
        }
    }
}
