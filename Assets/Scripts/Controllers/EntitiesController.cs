using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitiesController : MonoBehaviour
{
    public enum GameState
    {
        None,
        DefaultEnemies,
        Asteroids
    }

    [SerializeField, Header("Dependencies")]
    private EnemyController _enemyController;
    [SerializeField]
    private StarController _starController;
    [SerializeField]
    private AsteroidController _asteroidController;

    private GameState _gameState;

    public void ChangeGameState(GameState gameState)
    {
        StopAllCoroutines();
        _gameState = gameState;
        switch (_gameState)
        {
            case GameState.DefaultEnemies:
                StartCoroutine(GenerateDefaultEnemies());
                break;
            case GameState.Asteroids:
                StartCoroutine(GenerateAsteroid());
                break;
        }
    }

    public void GenerateStars()
    {
        for(int x = -8; x < 8; x += 2)
        {
            var star_possibility = Random.Range(0, 10);
            if (star_possibility != 0)
                continue;
            _starController.GenerateRandomStar(new Vector2(x, 6f));
        }
    }

    private IEnumerator GenerateDefaultEnemies()
    {
        yield return new WaitForSeconds(3);
        //returns position x 10 or -10
        float pos_x = (Random.Range(0, 2) * 2 - 1) * 10;
        float pos_y = Random.Range(1.5f, 4.5f);

        _enemyController.GenerateDefaultEnemy(new Vector2(pos_x, pos_y));

        StartCoroutine(GenerateDefaultEnemies());
    }

    private IEnumerator GenerateAsteroid()
    {
        yield return new WaitForSeconds(Random.Range(0.0f, 3.0f));

        float pos_x = Random.Range(-8f, 8f);
        float pos_y = 8;

        _asteroidController.GenerateRandomAsteroid(new Vector2(pos_x, pos_y));

        StartCoroutine(GenerateAsteroid());
    }
}
