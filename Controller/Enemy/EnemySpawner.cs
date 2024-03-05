using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;
    private int XPos;
    private int ZPos;
    public int EnemyCount;
    public int MaxEnemy;
    public int MinX, MaxX, MinZ, MaxZ;

    void Start()
    { 
        StartCoroutine(EnemyDrop());
    }


    IEnumerator EnemyDrop()
    {
        while (EnemyCount < MaxEnemy)
        {
            XPos = Random.Range(MinX, MaxX);
            ZPos = Random.Range(MinZ, MaxZ);
            Instantiate(Enemy, new Vector3(XPos, 0, ZPos), Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
            EnemyCount += 1;
        }
    }
    public void DecreaseEnemyCount()
    {
        EnemyCount--;
        if (EnemyCount<MaxEnemy)
        {
            StartCoroutine(EnemyDrop());
        }
    }
}
//x = 820,912
//z = 639,756

//907,821,826,928
//847,842,671,671