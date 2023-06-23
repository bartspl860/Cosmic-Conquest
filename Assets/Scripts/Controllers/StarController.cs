using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _starPrefabs;

    public void GenerateRandomStar(Vector2 pos)
    {
        var star = Instantiate(_starPrefabs[Random.Range(0, _starPrefabs.Length)]);
        star.transform.position = pos;
        star.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1f);
    }
}
