using System.Collections;
using UnityEngine;

namespace Controllers
{
    public class EnviromentController : MonoBehaviour
    {
        [SerializeField, Header("Game State Control")]
        private bool _asteroids;
        [SerializeField]
        private bool _defaultEnemies;

        [SerializeField, Header("Dependencies")]
        private EntitiesController _entitiesController;

        private void Start()
        {
            //StartCoroutine(MainSequence());
            StartCoroutine(Stars());
        }

        public void StopMainSequence()
        {
            _entitiesController.StopAll();
            StopAllCoroutines();
        }

        private void Update()
        {
            if( _asteroids )
            {
                _entitiesController.AddGameState(GameState.Asteroids);
            }
            else
            {
                _entitiesController.RemoveGameState(GameState.Asteroids);
            }
            if( _defaultEnemies)
            {
                _entitiesController.AddGameState(GameState.DefaultEnemies);
            }
            else
            {
                _entitiesController.RemoveGameState(GameState.DefaultEnemies);
            }
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
}
