using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Controllers
{
    public class AsteroidController : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] _asteroidPrefabs;

        private EnviromentController _enviromentController;

        private void Start()
        {
            _enviromentController = FindFirstObjectByType<EnviromentController>();
        }

        public void GenerateRandomAsteroid(Vector2 pos)
        {
            var asteroid = Instantiate(_asteroidPrefabs[Random.Range(0, _asteroidPrefabs.Length)]);
            asteroid.transform.position = pos;
        }
    }
}
