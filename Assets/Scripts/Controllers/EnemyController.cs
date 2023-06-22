using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private GameObject _defaultEnemyPrefab;

    public void GenerateDefaultEnemy(Vector2 pos)
    {
        var enemy = Instantiate(_defaultEnemyPrefab);
        enemy.transform.position = pos;        
    }
}
