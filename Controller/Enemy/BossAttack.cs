using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : AbstractEnemyAttack
{

    private static int DamageAmount = 20;

    public override void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            other.GetComponent<PlayerGetDamage>().TakeDamage(DamageAmount);
            Debug.Log("Kena tonjok");
        }
    }

    private void Start()
    {
        //DamageAmount = Boss.minAttack;
    }
    public override void OnTriggerStay(Collider other)
    {
        //throw new System.NotImplementedException();
    }


}
