using UnityEngine;

namespace Controllers
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField]
        private GameObject _defaultEnemyPrefab;

        public void GenerateDefaultEnemy(Vector2 pos)
        {
            var enemy = Instantiate(_defaultEnemyPrefab);
            enemy.transform.position = pos;        
        }
    }
}
