using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RhinoAttack : AbstractEnemyAttack
{
    public static int DamageAmount = 0;

    public override void OnTriggerEnter(Collider other)
    {

    }

    public override void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerGetDamage>().TakeDamage(DamageAmount);
            Debug.Log("kena tonjok");
        }
    }


    
}
