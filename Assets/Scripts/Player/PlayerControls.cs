using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    protected PlayerControls() { }

    [SerializeField, Header("Instance")]
    private GameObject _player;    
    [SerializeField, Header("Properties")]
    private float _playerSpeed;
    [SerializeField, Header("Dependencies")]
    private ShootingController _shootingController;

    private Rigidbody2D _playerRigidbody;
    private CapsuleCollider2D _playerCollider;

    public static Vector4 _screenBounds;

    private void Start()
    {
        _playerCollider = _player.GetComponent<CapsuleCollider2D>();
        _playerRigidbody = _player.GetComponent<Rigidbody2D>();
        
        _screenBounds = GetScreenBounds();
        StartCoroutine(Shooting(0.1f));       
    }

    private void Update()
    {
        ForcePlayerInBounds(_player.transform);
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

    public void PlayerShoot()
    {
        _shootingController.GenerateBullet(_player.transform.position);
    }

    IEnumerator Shooting(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        PlayerShoot();
        StartCoroutine(Shooting(waitTime));
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
}
