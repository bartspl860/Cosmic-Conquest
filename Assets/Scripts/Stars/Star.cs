using UnityEngine;

namespace Stars
{
    public class Star : MonoBehaviour
    {
        private void Update()
        {
            if(transform.position.y < -10f)
            {
                Destroy(gameObject);
            }
        }
    }
}
