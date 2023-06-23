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
    [SerializeField]
    private StarController _starController;

    private GameState _gameState;
    private List<Coroutine> _coroutine = new List<Coroutine>();

    private void Awake()
    {
        ChangeGameState(GameState.DefaultEnemies);
        StartCoroutine(CreateRandomRowOfStars());
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


    private IEnumerator CreateRandomRowOfStars()
    {
        yield return new WaitForSeconds(3);        

        for(int x = -8; x < 8; x+=2)
        {
            var star_possibility = Random.Range(0, 5);
            if (star_possibility != 0)
                continue;
            _starController.GenerateRandomStar(new Vector2(x, 6f));
        }
        
        StartCoroutine(CreateRandomRowOfStars());
    }

}
