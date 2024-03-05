using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public static int DamageAmount = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag =="Rhino")
        {
            other.GetComponent<Rhino>().TakeDamage(DamageAmount);
            Debug.Log("tonjok rhino");
        }
        if (other.tag == "IceElemental")
        {
            other.GetComponent<IceElemental>().TakeDamage(DamageAmount);
            Debug.Log("tonjok ice elemental");
        }
        if (other.tag == "FireElemental")
        {
            other.GetComponent<FireElemental>().TakeDamage(DamageAmount);
            Debug.Log("tonjok fire elemental");
        }
        if (other.tag == "EarthElemental")
        {
            other.GetComponent<EarthElemental>().TakeDamage(DamageAmount);
            Debug.Log("tonjok earth elemental");
        }
        if (other.tag == "Boss")
        {
            other.GetComponent<Boss>().TakeDamage(DamageAmount);
            Debug.Log("Tonjok Boss");
        }
        if (other.tag == "Minotaur")
        {
            other.GetComponent<Minotaur>().TakeDamage(DamageAmount);
            Debug.Log("Tonjok Minotaur");
        }



    }
}
