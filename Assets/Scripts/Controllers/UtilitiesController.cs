using UnityEngine;

namespace Controllers
{
    public class UtilitiesController : MonoBehaviour
    {
        [SerializeField] 
        private GameObject _bronzeShieldPrefab;

        public void SpawnBronzeShield(Vector2 pos)
        {
            var shield = Instantiate(_bronzeShieldPrefab);
            shield.transform.position = pos;
        }
    }
}