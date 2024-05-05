using System;
using UnityEngine;

namespace Utilities
{
    public class Utility : MonoBehaviour
    {
        private void Update()
        {
            var position = transform.position;
            if(position.y < -10)
            {
                Destroy(gameObject);
            }
        }
    }
}