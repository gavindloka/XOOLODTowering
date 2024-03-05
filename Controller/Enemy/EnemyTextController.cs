using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTextController : MonoBehaviour
{
    public GameObject Enemy;
    public GameObject HealthLevelCanvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 enemyPosition = Enemy.transform.position;
        Vector3 canvasOffset = new Vector3(0f, 8f, 0f);
        HealthLevelCanvas.transform.position = enemyPosition + canvasOffset;
    }
}

