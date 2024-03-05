using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node :MonoBehaviour
{
    public bool Walkable;
    public Vector2Int GridPosition;
    public int GridX;
    public int GridY;

    public int GCost;
    public float HCost;
    public Node Parent;

    public GameObject leftWall;
    public GameObject rightWall;
    public GameObject topWall;
    public GameObject bottomWall;
    public GameObject leftLight;
    public GameObject rightLight;
    public GameObject topLight;
    public GameObject bottomLight;  
    public bool delete = true;

    public float FCost
    {
        get
        {
            return GCost + HCost;
        }
    }
}
