using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceElementalAttack : AbstractEnemyAttack
{
    private IceElemental ie;
    public static int DamageAmount;

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
        DamageAmount = IceElemental.minAttack;

    }
    public override void OnTriggerStay(Collider other)
    {
        //throw new System.NotImplementedException();
    }
}
