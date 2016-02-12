using UnityEngine;
using System.Collections;
using System;

public class SwordWeapon : Weapon {

    public SwordWeapon(string name, string desc, int worth, int amount = 1) : base(name, desc, worth, amount)
    {
        
    }


    public override void Attack()
    {
        Debug.Log("Attack!");
    }

}
