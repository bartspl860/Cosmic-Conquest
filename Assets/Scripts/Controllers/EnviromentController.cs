using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnviromentController : MonoBehaviour
{
    [SerializeField, Header("Dependencies")]
    private EntitiesController _entitiesController;

    private void Start()
    {
        //StartCoroutine(MainSequence());
        StartCoroutine(Stars());
        _entitiesController.AddGameState(GameState.Asteroids);
    }

    public void StopMainSequence()
    {
        _entitiesController.StopAll();
        StopAllCoroutines();
    }

    private IEnumerator MainSequence()
    {
        while (true)
        {
            var seq = Random.Range(0, 2);
            switch (seq)
            {
                case 0: _entitiesController.AddGameState(GameState.Asteroids, GameState.DefaultEnemies); break;
                case 1: _entitiesController.AddGameState(GameState.Asteroids); break;
                case 2: _entitiesController.AddGameState(GameState.DefaultEnemies); break;
            }
            yield return new WaitForSeconds(Random.Range(5f, 30f));

            _entitiesController.StopAll();
        }       
    }

    private IEnumerator Stars()
    {
        _entitiesController.GenerateStars();
        yield return new WaitForSeconds(3f);
        StartCoroutine(Stars());
    }
}
