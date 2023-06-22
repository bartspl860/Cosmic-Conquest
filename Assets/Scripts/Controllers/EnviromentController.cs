using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentController : MonoBehaviour
{
    public enum GameState
    {
        Asteroids,
        DefaultEnemies
    }

    [SerializeField, Header("Dependencies")]
    private EnemyController _enemyController;

    private GameState _gameState;
    private List<Coroutine> _coroutine = new List<Coroutine>();

    private void Start()
    {
        ChangeGameState(GameState.DefaultEnemies);
    }

    private void ChangeGameState(GameState gameState)
    {
        if(_coroutine != null)
        {
            _coroutine.ForEach(c=>StopCoroutine(c));
        }
        _gameState = gameState;
        switch (_gameState)
        {
            case GameState.Asteroids:
                    
            break; 
            case GameState.DefaultEnemies:
                _coroutine.Add(StartCoroutine(GenerateDefaultEnemies()));    
            break;
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




}
