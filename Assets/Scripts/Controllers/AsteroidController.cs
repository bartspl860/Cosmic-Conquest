using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    [SerializeField]
    private GameObject _defaultAsteroidPrefab;

    public void GenerateDefaultEnemy(Vector2 pos)
    {
        var asteroid = Instantiate(_defaultAsteroidPrefab);
        asteroid.transform.position = pos;
    }
}
