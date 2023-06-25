using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _asteroidPrefabs;

    public void GenerateRandomAsteroid(Vector2 pos)
    {
        var asteroid = Instantiate(_asteroidPrefabs[Random.Range(0, _asteroidPrefabs.Length)]);
        asteroid.transform.position = pos;
    }
}
