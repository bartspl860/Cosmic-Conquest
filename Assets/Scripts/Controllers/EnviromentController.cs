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
        [SerializeField, Header("Dependencies")]
        private EntitiesController _entitiesController;


        private void Start()
        {
            SpawnDefaultEnemies();
            // Spawn30Asteroids();
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
