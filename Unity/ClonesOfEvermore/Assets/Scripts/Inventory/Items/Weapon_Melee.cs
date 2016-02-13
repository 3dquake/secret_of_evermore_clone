using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Generic melee weapon
/// </summary>
public class Weapon_Melee : Weapon {
    
    float m_time;
    float m_next;
    public float attackInterval = 2;
    bool m_canAttack = true;

    public Weapon_Melee()
        : base()
    {

    }

    public Weapon_Melee(string name, string desc, int worth) 
        : base(name, desc, worth)
    {
        
    }

    public override void Think()
    {
        if (!m_canAttack)
        {
            if (m_time > Time.time)
                m_next += Time.deltaTime;
        }
    }

    public override void Attack()
    {
        if (m_canAttack)
        {
            Debug.Log("Punch!");
            m_canAttack = false;
            m_time = Time.time + attackInterval;
        }
    }

    public override object Clone()
    {
        return new Weapon_Melee(Name, Description, Worth);
    }

}
