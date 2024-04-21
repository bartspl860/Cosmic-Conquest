using System;
using System.Collections;
using System.ComponentModel;
using Controllers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Controllers
{
    public class EnviromentController : MonoBehaviour
    {
        [SerializeField, Header("Dependencies")]
        private EntitiesController _entitiesController;
        

        private void Start()
        {
            //Programmable sequence of game states
            StartCoroutine(MainSequence());
            StartCoroutine(Stars());
            
        }

        public void StopMainSequence()
        {
            _entitiesController.StopAll();
            StopAllCoroutines();
        }

        private IEnumerator MainSequence()
        {
            _entitiesController.AddGameState(GameState.Asteroids);
            while (true)
            {
                // var seq = Random.Range(0, 2);
                // switch (seq)
                // {
                //     case 0: _entitiesController.AddGameState(GameState.Asteroids, GameState.DefaultShips); break;
                //     case 1: _entitiesController.AddGameState(GameState.Asteroids); break;
                //     case 2: _entitiesController.AddGameState(GameState.DefaultShips); break;
                // }
                
                // _entitiesController.AddGameState(GameState.SnakeShips);
                yield return new WaitForSeconds(5f);
                // _entitiesController.StopAll();
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
