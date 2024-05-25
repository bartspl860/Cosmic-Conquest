using System;
using System.Collections;
using System.ComponentModel;
using Controllers;
using Http;
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
        private bool _allowSpawning = true;
        private int _bossSpawnPoints = 5000;

        private void Start()
        {
            _player = FindFirstObjectByType<Player.Player>();
            StartCoroutine(CheckIfSceneIsEmpty());
        }

        private IEnumerator CheckIfSceneIsEmpty()
        {
            var entitiesCount = GameObject.FindGameObjectsWithTag("Destroyable").Length;
            
            if (entitiesCount < 1)
                _allowSpawning = true;

            if (_allowSpawning && !_entitiesController.IsGenerating())
            {
                SpawnEntities();
            }
            
            yield return new WaitForSeconds(3);

            StartCoroutine(CheckIfSceneIsEmpty());
        }

        private void SpawnEntities()
        {
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
                    _entitiesController.SpawnAsteroids(Random.Range(10, 30), 2f);
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
    }
}
