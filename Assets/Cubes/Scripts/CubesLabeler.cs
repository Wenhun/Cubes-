using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CubesLabeler : MonoBehaviour
{

    Vector2Int coordinates = new Vector2Int();
   
    void Update()
    {
        LabelCubes();
    }

    void LabelCubes()
    {
        coordinates.x = Mathf.RoundToInt(transform.position.x);
        coordinates.y = Mathf.RoundToInt(transform.position.y);

        transform.name = coordinates.x + ", " + coordinates.y;

    }
}
