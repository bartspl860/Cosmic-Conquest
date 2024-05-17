using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Controllers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Controllers
{
    public enum EntityType
    {
        DefaultShips,
        SnakeShips,
        Asteroids
    }

    public class EntitiesController : MonoBehaviour
    {
        [SerializeField, Header("Dependencies")]
        private EnemyController enemyController;
        
        [SerializeField] AsteroidController asteroidController;

        private bool _generating = false;

        public bool IsGenerating()
        {
            return _generating;
        }
        public void SpawnDefaultShips(int count, float delay)
        {
            _generating = true;
            StartCoroutine(GenerateDefaultShips(count, delay));
        }
        public void SpawnAsteroids(int count, float delay)
        {
            _generating = true;
            StartCoroutine(GenerateAsteroid(count, delay));
        }
        
        public void SpawnSnakeShips(int count, float delay)
        {
            _generating = true;
            StartCoroutine(GenerateSnakeShips(count, delay));
        }
        public void SpawnBoss(int count)
        {
            float pos_x = (Random.Range(0, 2) * 2 - 1) * 10;
            float pos_y = Random.Range(1.5f, 4.5f);
            enemyController.GenerateBoss(new Vector2(pos_x, pos_y));
        }
        
        private IEnumerator GenerateDefaultShips(int count, float delay)
        {
            
            for (var i = 0; i < count; i++)
            {
                //returns position x 10 or -10
                float pos_x = (Random.Range(0, 2) * 2 - 1) * 10;
                float pos_y = Random.Range(1.5f, 4.5f);

                enemyController.GenerateDefaultEnemy(new Vector2(pos_x, pos_y));
                yield return new WaitForSeconds(delay);
            }
            _generating = false;
        }

        private IEnumerator GenerateSnakeShips(int count, float delay)
        {
            float pos_x = (Random.Range(0, 2) * 2 - 1) * 10;
            float pos_y = Random.Range(1.5f, 4.5f);

            var twoSideSpawn = Random.Range(0, 1) > 0.5f;

            for (var i = 0; i < count; i++)
            {
                if (twoSideSpawn) pos_x *= -1;
                var position = new Vector2(pos_x, pos_y) + (Vector2)Random.insideUnitSphere * 0.5f;
                enemyController.GenerateSnakeEnemy(position);
                yield return new WaitForSeconds(delay);  
            }
            _generating = false;
        }

        private IEnumerator GenerateAsteroid(int count, float delay)
        {
            for (var i = 0; i < count; i++)
            {
                float pos_x = Random.Range(-8f, 8f);
                float pos_y = 8;

                asteroidController.GenerateRandomAsteroid(new Vector2(pos_x, pos_y));
                
                yield return new WaitForSeconds(Random.Range(0.0f, delay));
            }
            _generating = false;
        }
    }
}