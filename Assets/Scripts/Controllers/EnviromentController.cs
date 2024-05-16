using System;
using System.Collections;
using System.ComponentModel;
using Controllers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Controllers
{
    public class EnviromentController : MonoBehaviour
    {
        enum EnemyType
        {
            Asteroid,
            Ship,
            SnakeShip
        }; 
            
        [SerializeField, Header("Dependencies")]
        private EntitiesController _entitiesController;

        private Player.Player _player;

        public int EntityCounter { get; set; }
        private bool _allowSpawning = true;

        private int _bossSpawnPoints = 5000;

        private void Start()
        {
            _player = FindFirstObjectByType<Player.Player>();
        }

        public void EntityDestroyed()
        {
            EntityCounter--;
            Debug.Log($"Destroyed enemy - 1: {EntityCounter}");
            if (EntityCounter < 1)
                _allowSpawning = true;
        }

        private void Update()
        {
            if (!_allowSpawning || _entitiesController.IsGenerating())
                return;
            
            if (_player.GetScore() > _bossSpawnPoints)
            {
                _entitiesController.SpawnBoss(1);
                _bossSpawnPoints += _bossSpawnPoints;
                _allowSpawning = false;
                return;
            }

            var enemyType = (EnemyType)(Random.Range(0, 3));

            switch (enemyType)
            {
                case EnemyType.Asteroid: 
                    _entitiesController.SpawnAsteroids(Random.Range(10, 30));
                    break;
                case EnemyType.Ship: 
                    _entitiesController.SpawnDefaultShips(Random.Range(5, 10), 1f);
                    break;
                case EnemyType.SnakeShip: 
                    _entitiesController.SpawnSnakeShips(Random.Range(5, 15), 0.4f);
                    break;
            }
            _allowSpawning = false;
        }

        private void SpawnDefaultEnemies()
        {
            _entitiesController.SpawnDefaultShips(80, 2f);
        }
        
        private void Spawn30Asteroids()
        {
            _entitiesController.SpawnAsteroids(30);
        }
    }
}
