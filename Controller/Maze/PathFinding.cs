using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    GenerateMaze grid;

    void Awake()
    {
        grid = GetComponent<GenerateMaze>();
    }
    void FindPath(Vector3 startPos, Vector3 targetPos)
    {
       //Vector2Int start = grid.roomdoorps

    }
}
