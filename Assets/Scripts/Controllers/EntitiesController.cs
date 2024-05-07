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

        public void SpawnDefaultShips(int count, float delay)
        {
            StartCoroutine(GenerateDefaultShips(count, delay));
        }
        public void SpawnAsteroids(int count)
        {
            StartCoroutine(GenerateAsteroid(count));
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
        }

        private IEnumerator GenerateSnakeShips(int count, float delay)
        {
            for (var i = 0; i < count; i++)
            {
                float pos_x = (Random.Range(0, 2) * 2 - 1) * 10;
                float pos_y = Random.Range(1.5f, 4.5f);
                
                enemyController.GenerateSnakeEnemy(new Vector2(pos_x, pos_y));
                yield return new WaitForSeconds(delay);  
            }
        }

        private IEnumerator GenerateAsteroid(int count)
        {
            for (var i = 0; i < count; i++)
            {
                float pos_x = Random.Range(-8f, 8f);
                float pos_y = 8;

                asteroidController.GenerateRandomAsteroid(new Vector2(pos_x, pos_y));
                
                yield return new WaitForSeconds(Random.Range(0.0f, 3.0f));
            }
        }
    }
}