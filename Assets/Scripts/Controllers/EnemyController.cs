using UnityEngine;
using UnityEngine.Serialization;

namespace Controllers
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField]
        private GameObject _enemyShipPrefab;
        [SerializeField] 
        private GameObject _enemySnakeShipPrefab;

        public void GenerateDefaultEnemy(Vector2 pos)
        {
            var enemy = Instantiate(_enemyShipPrefab);
            enemy.transform.position = pos;        
        }
    }
}
