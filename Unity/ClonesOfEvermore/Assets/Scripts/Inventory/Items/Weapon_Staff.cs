using System;
using UnityEngine;

public class Weapon_Staff : Weapon 
{
    public Weapon_Staff() 
        : base()
    {

    }

    public Weapon_Staff(string name, string desc, int worth) 
        : base(name, desc, worth)
    {

    }

    public override void Attack()
    {
        Debug.Log("Zap!");
    }

    public override object Clone()
    {
        return MemberwiseClone();
    }
}

