using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyModel : MonoBehaviour
{
    public int Level { get; private set; }

    public void SetLevel(int level)
    {
        Level = level;
    }
}
