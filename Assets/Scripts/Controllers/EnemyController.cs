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

        private EnviromentController _enviromentController;

        private void Start()
        {
            _enviromentController = FindFirstObjectByType<EnviromentController>();
        }

        public void GenerateDefaultEnemy(Vector2 pos)
        {
            var enemy = Instantiate(_enemyShipPrefab);
            _enviromentController.EntityCounter+=1;
            Debug.Log($"New Enemy + 1: {_enviromentController.EntityCounter}");
            enemy.transform.position = pos;        
        }
        public void GenerateSnakeEnemy(Vector2 pos)
        {
            var enemy = Instantiate(_enemySnakeShipPrefab);
            _enviromentController.EntityCounter+=1;
            Debug.Log($"New Enemy + 1: {_enviromentController.EntityCounter}");
            enemy.transform.position = pos;        
        }
        public void GenerateBoss(Vector2 pos)
        {
            var enemy = Instantiate(_bossPrefab);
            _enviromentController.EntityCounter+=1;
            Debug.Log($"New Enemy + 1: {_enviromentController.EntityCounter}");
            enemy.transform.position = pos;        
        }
    }
}