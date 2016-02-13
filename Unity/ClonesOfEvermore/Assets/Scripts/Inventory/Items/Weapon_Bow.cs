using System;
using UnityEngine;

public class Weapon_Bow : Weapon
{
    public Weapon_Bow()
        : base()
    {

    }

    public Weapon_Bow(string name, string desc, int worth) : base(name, desc, worth)
    {

    }

    public override void Attack()
    {
        Debug.Log("Shoot!");
    }

    public override object Clone()
    {
        return new Weapon_Bow(Name, Description, Worth);
    }
}