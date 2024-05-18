using System;
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
        [SerializeField] 
        private GameObject _bossPrefab;

        public void GenerateDefaultEnemy(Vector2 pos)
        {
            var enemy = Instantiate(_enemyShipPrefab);
            enemy.transform.position = pos;        
        }
        public void GenerateSnakeEnemy(Vector2 pos)
        {
            var enemy = Instantiate(_enemySnakeShipPrefab);
            enemy.transform.position = pos;        
        }
        public void GenerateBoss(Vector2 pos)
        {
            var enemy = Instantiate(_bossPrefab);
            enemy.transform.position = pos;        
        }
    }
}