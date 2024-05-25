using System;
using System.Collections;
using System.Linq;
using Controllers;
using Effects;
using Http;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class Player : MonoBehaviour
    {
        protected Player() { }

        [SerializeField, Header("Instance")]
        private GameObject _player;    
        [SerializeField, Header("Properties")]
        private float _playerSpeed;
        [SerializeField]
        private float _playerRotationAngle;
        [SerializeField]
        private float _shootingSpeed;
        [SerializeField] 
        private int _missilesCount;
        [SerializeField]
        private int _playerHealth;
        [SerializeField]
        private int _playerMaxHealth;
        [SerializeField]
        private bool _shield;
        [SerializeField]
        private float _shieldDuration;
        [SerializeField]
        private float _shieldTriggerTime;
        [SerializeField]
        private int _score;
        [SerializeField]
        private bool _invincible = false;
        [SerializeField, Header("Dependencies")]
        private BulletController _bulletController;
        [SerializeField]
        private UIController _uiController;
        [SerializeField]
        private Shaking _screenShaking;
        [SerializeField] 
        private Twinkling _playerTwinkling;
        [SerializeField]
        private Fading _gameOverTMPfading;
        [SerializeField]
        private SpriteRenderer _shieldRenderer;
        [SerializeField]
        private Animator _shieldAnimator;
        [SerializeField] 
        private GameOverController _gameOverController;

        private Rigidbody2D _playerRigidbody;

        public static Vector4 _screenBounds;

        private void Start()
        {
            _playerRigidbody = _player.GetComponent<Rigidbody2D>();
            _uiController.DisplayHealthPoints(_playerHealth, _playerMaxHealth);
            _screenBounds = GetScreenBounds();
            StartCoroutine(IEShooting());
        }

        private bool _shieldGuard = false;
        private void Update()
        {
            ForcePlayerInBounds(_player.transform);
            if(_shield && !_shieldGuard)
            {
                _shieldGuard = true;
                ActivateShield();          
            }
        }

        private Coroutine _shieldCoroutine;

        public void AddMissilesCount(int count)
        {
            if(_missilesCount + count < 1 || _missilesCount + count > 5)
                return;
            this._missilesCount += count;
        }

        public void AddHealth(int count)
        {
            this._playerHealth = Math.Clamp(_playerHealth + count, 1, 10);
            _uiController.DisplayHealthPoints(_playerHealth, _playerMaxHealth);
        }
        
        public void ActivateShield()
        {
            _shield = true;
            if(_shieldCoroutine != null)
                StopCoroutine(_shieldCoroutine);
            _shieldCoroutine = StartCoroutine(IEActivateShield());
        }

        public void TriggerShield()
        {
            StartCoroutine(IETriggerShield());
        }

        private IEnumerator IETriggerShield() {
        
            var elapsedTime = 0f;

            var halfTriggerTime = _shieldTriggerTime / 2f;

            while (elapsedTime < halfTriggerTime)
            {
                elapsedTime += Time.deltaTime;
                _shieldRenderer.color = Color.Lerp(Color.white, new Color(1.0f, 165/255.0f, 0.0f, 1.0f), elapsedTime / halfTriggerTime);
                yield return null;
            }

            elapsedTime = 0f;

            while (elapsedTime < halfTriggerTime)
            {
                elapsedTime += Time.deltaTime;
                _shieldRenderer.color = Color.Lerp(new Color(1.0f, 165 / 255.0f, 0.0f, 1.0f), Color.white, elapsedTime / halfTriggerTime);
                yield return null;
            }        
        }

        private IEnumerator IEActivateShield()
        {
            var shieldEndingTime = _shieldDuration * 0.30f;
            _shieldAnimator.SetBool("Shield", true);

            yield return new WaitForSeconds(_shieldDuration - shieldEndingTime);

            _shieldAnimator.SetBool("Shield_Ending", true);

            yield return new WaitForSeconds(shieldEndingTime);

            _shieldAnimator.SetBool("Shield", false);
            _shieldAnimator.SetBool("Shield_Ending", false);
            _shield = false;
            _shieldGuard = false;
        }

        private IEnumerator IEActivateInvincibility(float duration)
        {
            _invincible = true;
            _playerTwinkling.StartTwinkling(frequency: 15f);
            yield return new WaitForSeconds(duration);
            _invincible = false;
            _playerTwinkling.StopTwinkling();
        }
        public void TemporaryInvincibility(float seconds)
        {
            if(_invincible)
                return;
            StartCoroutine(IEActivateInvincibility(seconds));
        }
        public void TakeDamage()
        {   
            if(_invincible || _shield)
                return;
            _screenShaking.startShake();
            _playerHealth--;
            if(_missilesCount > 1)
                _missilesCount--;
            _uiController.DisplayHealthPoints(_playerHealth, _playerMaxHealth);
            TemporaryInvincibility(1);
            if ( _playerHealth < 1 ) 
            {
                _gameOverTMPfading.startFade();
                _gameOverController.CheckIfScoreTop10();
                Destroy(_player);
                Destroy(gameObject);
            }
        }

        public void AddScore(int score)
        {
            _score += score;
            _uiController.DisplayScore(_score);
        }
        
        public int GetScore()
        {
            return _score;
        }

        private void ForcePlayerInBounds(Transform t)
        {
            if(t.position.x < _screenBounds.x)
            {
                t.position = new Vector2(_screenBounds.x, t.position.y);
            }
            else if(t.position.x > _screenBounds.y)
            {
                t.position = new Vector2(_screenBounds.y, t.position.y);
            }
            if (t.position.y < _screenBounds.z)
            {
                t.position = new Vector2(t.position.x, _screenBounds.z);
            }
            else if (t.position.y > _screenBounds.w)
            {
                t.position = new Vector2(t.position.x, _screenBounds.w);
            }
        }

        private void PlayerShoot(int missiles)
        {
            var pos = _player.transform.position;
            pos.y += 0.3f;

            var maxShootingAngle = Math.Clamp(_missilesCount * 10, 0, 45);

            var shootingPie = 2f * maxShootingAngle;
            var shootingPiePart = shootingPie / missiles;
            var angleOffset = 0f;
            
            // Adjust for odd number of missiles
            if (missiles % 2 != 0)
            {
                angleOffset = shootingPiePart / 2;
            }

            var angle = -maxShootingAngle + angleOffset;
            
            if (missiles % 2 == 0)
            {
                angle += shootingPiePart / 2; // Adjust starting angle for even numbers
            }
            
            for (var i = 0; i < missiles; i++)
            {
                _bulletController.GeneratePlayerBullet(pos, Quaternion.Euler(0, 0, angle));
                angle += shootingPiePart;
            }
        }
        
        IEnumerator IEShooting()
        {
            while (true)
            {
                yield return new WaitForSeconds(_shootingSpeed);
                PlayerShoot(_missilesCount);
            }
        }

        private void FixedUpdate()
        {
            // Get input axes
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            // Calculate movement vector
            Vector2 movement = new Vector2(moveHorizontal, moveVertical);

            // Apply movement
            _playerRigidbody.velocity = movement * _playerSpeed;

            //Rotate Ship
            float _playerRotation = moveHorizontal * -_playerRotationAngle;
            _player.transform.rotation = Quaternion.Euler(new Vector3(0, 0, _playerRotation));
        }

        private Vector4 GetScreenBounds()
        {
            float objectWidth = transform.localScale.x;
            float objectHeight = transform.localScale.y;

            float screenAspect = (float)Screen.width / Screen.height;
            float cameraHeight = Camera.main.orthographicSize * 2;
            float cameraWidth = cameraHeight * screenAspect;

            float xMin = Camera.main.transform.position.x - cameraWidth / 2f + objectWidth / 2f;
            float xMax = Camera.main.transform.position.x + cameraWidth / 2f - objectWidth / 2f;
            float yMin = Camera.main.transform.position.y - cameraHeight / 2f + objectHeight / 2f;
            float yMax = Camera.main.transform.position.y + cameraHeight / 2f - objectHeight / 2f;

            return new Vector4(xMin, xMax, yMin, yMax);
        }

        public bool IsShielded  => _shield; 
        public bool IsInvincible => _invincible;
    }
}
