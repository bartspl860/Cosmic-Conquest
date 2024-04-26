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

        private IEnumerator GenerateDefaultShips()
        {
            while (true)
            {
                yield return new WaitForSeconds(3);
                //returns position x 10 or -10
                float pos_x = (Random.Range(0, 2) * 2 - 1) * 10;
                float pos_y = Random.Range(1.5f, 4.5f);

                enemyController.GenerateDefaultEnemy(new Vector2(pos_x, pos_y));
            }
        }

        private IEnumerator GenerateSnakeShips()
        {
            float pos_x = (Random.Range(0, 2) * 2 - 1) * 10;
            float pos_y = Random.Range(1.5f, 4.5f);
            Debug.Log((pos_x, pos_y));
            for (var i = 0; i < 15; i++)
            {
                enemyController.GenerateSnakeEnemy(new Vector2(pos_x, pos_y));
                yield return new WaitForSeconds(0.6f);   
            }
        }

        private IEnumerator GenerateAsteroid()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(0.0f, 3.0f));

                float pos_x = Random.Range(-8f, 8f);
                float pos_y = 8;

                asteroidController.GenerateRandomAsteroid(new Vector2(pos_x, pos_y));
            }
        }
    }
}