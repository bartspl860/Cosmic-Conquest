using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public enum GameState
{
    None,
    DefaultEnemies,
    Asteroids
}
public class EntitiesController : MonoBehaviour
{    
    [SerializeField, Header("Dependencies")]
    private EnemyController _enemyController;
    [SerializeField]
    private StarController _starController;
    [SerializeField]
    private AsteroidController _asteroidController;

    private readonly Dictionary<GameState, IEnumerator> _gameStates = new Dictionary<GameState, IEnumerator>();
    private List<GameState> _currentlyRunningGameStates = new List<GameState>();

    public List<GameState> RunningGameStates { get => _currentlyRunningGameStates; }

    private void Start()
    {
        _gameStates.Add(GameState.Asteroids, GenerateAsteroid());
        _gameStates.Add(GameState.DefaultEnemies, GenerateDefaultEnemies());
    }

    public void AddGameState(params GameState[] states)
    {
        foreach(var state in states)
        {            
            if (!_currentlyRunningGameStates.Contains(state))
            {
                _currentlyRunningGameStates.Add(state);                
                StartCoroutine(_gameStates[state]);                
            }                
        } 
    }

    public void RemoveGameState(params GameState[] states)
    {
        foreach(var state in states)
        {
            _currentlyRunningGameStates.Remove(state);
            StopCoroutine(_gameStates[state]);
        }       
    }

    public void StopAll()
    {
        _currentlyRunningGameStates.Clear();
        StopAllCoroutines();
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
        while (true)
        {
            yield return new WaitForSeconds(3);
            //returns position x 10 or -10
            float pos_x = (Random.Range(0, 2) * 2 - 1) * 10;
            float pos_y = Random.Range(1.5f, 4.5f);

            _enemyController.GenerateDefaultEnemy(new Vector2(pos_x, pos_y));
        }
    }

    private IEnumerator GenerateAsteroid()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0.0f, 3.0f));

            float pos_x = Random.Range(-8f, 8f);
            float pos_y = 8;

            _asteroidController.GenerateRandomAsteroid(new Vector2(pos_x, pos_y));
        }
    }
}
