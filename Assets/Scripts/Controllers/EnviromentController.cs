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
        StartCoroutine(MainSequence());
        StartCoroutine(Stars());
    }

    private IEnumerator MainSequence()
    {
        _entitiesController.ChangeGameState(EntitiesController.GameState.Asteroids);
        yield return new WaitForSeconds(20);
        _entitiesController.ChangeGameState(EntitiesController.GameState.DefaultEnemies);
        yield return new WaitForSeconds(20);
        StartCoroutine(MainSequence());
    }

    private IEnumerator Stars()
    {
        _entitiesController.GenerateStars();
        yield return new WaitForSeconds(3f);
        StartCoroutine(Stars());
    }
}
