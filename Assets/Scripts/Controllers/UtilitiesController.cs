using UnityEngine;

namespace Controllers
{
    public class UtilitiesController : MonoBehaviour
    {
        [SerializeField] 
        private GameObject _bronzeShieldPrefab;

        [SerializeField] 
        private GameObject _bronzeAmmoPrefab;

        public void SpawnBronzeShield(Vector2 pos)
        {
            var shield = Instantiate(_bronzeShieldPrefab);
            shield.transform.position = pos;
        }
        
        public void SpawnBronzeAmmo(Vector2 pos)
        {
            var shield = Instantiate(_bronzeAmmoPrefab);
            shield.transform.position = pos;
        }
    }
}