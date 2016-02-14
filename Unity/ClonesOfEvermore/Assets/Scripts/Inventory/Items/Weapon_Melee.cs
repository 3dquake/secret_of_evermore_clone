using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Generic melee weapon
/// </summary>
public class Weapon_Melee : Weapon {
    
    float m_next;
    public float attackInterval = 1f;
    bool m_canAttack = true;

    public Weapon_Melee()
        : base()
    {
        
    }

    public Weapon_Melee(string name, string desc, int worth) 
        : base(name, desc, worth)
    {
        
    }

    public override void OnEquip()
    {
        Damage = 10;
    }

    public override void Think()
    {
        if (!m_canAttack)
        {
            m_next -= Time.deltaTime;
            if (m_next <= 0)
                m_canAttack = true;
        }
    }

    void Punch()
    {
        Collider[] hits = Physics.OverlapBox(Owner.Link.transform.position + Owner.Link.transform.forward * 1.2f, Vector3.one);
        foreach (Collider hit in hits)
        {
            VisualCharacter character = hit.GetComponent<VisualCharacter>();
            if (character != null && character != Owner.Link)
            {
                if (character.Link == Owner)
                    Debug.LogFormat("{0}({1}) punched self", Owner.Link.name, Owner.Health);
                else
                    Debug.LogFormat("{0}({1}) punched {2}({3})", Owner.Link.name, Owner.Health, character.name, character.Link.Health);

                character.Hurt(Damage);
                break;
            }

        }
    }

    public override void Attack()
    {
        if (m_canAttack)
        {
            m_next += attackInterval;
            m_canAttack = false;

            Punch();

        }
    }

    public override object Clone()
    {
        return MemberwiseClone();/*new Weapon_Melee(Name, Description, Worth);*/
    }

}
