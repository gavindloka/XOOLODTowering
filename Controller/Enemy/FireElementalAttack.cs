using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class FireElementalAttack : AbstractEnemyAttack
{
    public static int DamageAmount;
    private void Start()
    {
        //fe = GetComponent<FireElemental>();
        //DamageAmount = fe.MinAttack;
        DamageAmount = FireElemental.minAttack;
    }
    public override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerGetDamage>().TakeDamage(DamageAmount);
            Debug.Log("kena tonjok");
        }
    }
    public override void OnTriggerStay(Collider other)
    {
        //throw new System.NotImplementedException();
    }
}

