using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurAttack : AbstractEnemyAttack
{
    public static int DamageAmount = 30;
    public override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerGetDamage>().TakeDamage(DamageAmount);
            Debug.Log("kena tonjok");
        }
    }
    private void Start()
    {
        //ie = GetComponent<IceElemental>();
        //DamageAmount = ie.MinAttack;
        DamageAmount = Minotaur.minAttack;

    }
    public override void OnTriggerStay(Collider other)
    {
    }

}
