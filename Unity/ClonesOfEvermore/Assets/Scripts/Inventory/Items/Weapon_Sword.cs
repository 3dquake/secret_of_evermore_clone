using UnityEngine;
using System.Collections;
using System;

public class Weapon_Sword : Weapon {

    public Weapon_Sword()
        : base()
    {

    }


    public Weapon_Sword(string name, string desc, int worth) 
        : base(name, desc, worth)
    {
        
    }

    public override void Attack()
    {
        Debug.Log("Swoosh!");
    }

    public override object Clone()
    {
        return new Weapon_Sword(Name, Description, Worth);
    }
}
