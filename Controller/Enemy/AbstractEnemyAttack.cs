using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class AbstractEnemyAttack : MonoBehaviour
{
    public abstract void OnTriggerEnter(Collider other);
    public abstract void OnTriggerStay(Collider other);
}
