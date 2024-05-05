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
            Spawn10DefaultEnemies();
        }

        private void Spawn10DefaultEnemies()
        {
            _entitiesController.SpawnDefaultShips(10, 2f);
        }
    }
}
