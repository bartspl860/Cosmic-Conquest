using System.Collections;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Serialization;

namespace Controllers
{
    public class EnviromentController : MonoBehaviour
    {
        [SerializeField, Header("Game State Control")]
        private bool _asteroids;
        [SerializeField]
        private bool _defaultShips;
        [SerializeField] 
        private bool _snakeShapedDefaultShips;
        
        [SerializeField, Header("Dependencies")]
        private EntitiesController _entitiesController;

        private void Start()
        {
            _snakeShapedDefaultShips = true;
            //Programmable sequence of game states
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
            if( _defaultShips)
            {
                _entitiesController.AddGameState(GameState.DefaultShips);
            }
            else
            {
                _entitiesController.RemoveGameState(GameState.DefaultShips);
            }
            if( _snakeShapedDefaultShips)
            {
                _entitiesController.AddGameState(GameState.SnakeShips);
            }
            else
            {
                _entitiesController.RemoveGameState(GameState.SnakeShips);
            }
        }

        private IEnumerator MainSequence()
        {
            while (true)
            {
                var seq = Random.Range(0, 2);
                switch (seq)
                {
                    case 0: _entitiesController.AddGameState(GameState.Asteroids, GameState.DefaultShips); break;
                    case 1: _entitiesController.AddGameState(GameState.Asteroids); break;
                    case 2: _entitiesController.AddGameState(GameState.DefaultShips); break;
                }
                yield return new WaitForSeconds(Random.Range(5f, 30f));

                _entitiesController.StopAll();
            }       
        }

        private IEnumerator Stars()
        {
            while (true)
            {
                _entitiesController.GenerateStars();
                yield return new WaitForSeconds(3f);
            }
        }
    }
}
