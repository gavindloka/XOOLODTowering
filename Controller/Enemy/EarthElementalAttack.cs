using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthElementalAttack : AbstractEnemyAttack
{
    private EarthElemental ee;
    public static int DamageAmount;
    private void Start()
    {
        //ee = GetComponent<EarthElemental>();
        //DamageAmount = ee.MinAttack;
        DamageAmount = EarthElemental.minAttack;
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
