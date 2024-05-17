using System;
using UnityEngine;

namespace Utilities
{
    public class Utility : MonoBehaviour
    {
        public enum Type { BrShield, BrAmmo, Health }

        [SerializeField] private Type _type;
        private void Update()
        {
            if(transform.position.y < -10)
            {
                Destroy(gameObject);
            }
        }
        private void Start()
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -1f);
        }
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                var player = FindFirstObjectByType<Player.Player>();
                switch (_type)
                {
                    case Type.BrShield:
                        player.ActivateShield();
                        break;
                    case Type.BrAmmo:
                        player.AddMissilesCount(1);
                        break;
                    case Type.Health:
                        player.AddHealth(1);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
                Destroy(gameObject);
            }
        }
    }
}