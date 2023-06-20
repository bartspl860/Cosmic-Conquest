using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControls : MonoBehaviour
{
    public Vector2 Bounds { get; set; }

    private void Start()
    {
        Bounds = new Vector2(
            PlayerControls._screenBounds.z, 
            PlayerControls._screenBounds.w
        );
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
        //if (collision.CompareTag("Destroyable"))
        //{
            //Destroy(collision.gameObject);
            //Destroy (this);
        //}
    }
}
