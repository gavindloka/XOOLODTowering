using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhinoCombat : MonoBehaviour
{
    private Rhino rhino;
    private void Start()
    {
        rhino = GetComponent<Rhino>();
    }
    public void Update()
    {
        Debug.Log("Rhino att"+rhino.MinAttack);
    }
    //IEnumerator stopAtt()
    //{
    //    yield return new WaitForSeconds(0.0000001f);
    //    EnemyAttack.DamageAmount = 0;
    //}
    public void DamageEnabled()
    {
        RhinoAttack.DamageAmount = rhino.MinAttack;
        //stopAtt();
    }
    public void DamageDisabled() {
        RhinoAttack.DamageAmount = 0;
    }
}
