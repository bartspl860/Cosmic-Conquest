using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    private void Update()
    {
        if(transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
    }
}
