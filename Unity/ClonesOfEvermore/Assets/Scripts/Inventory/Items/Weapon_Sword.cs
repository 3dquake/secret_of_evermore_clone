using UnityEngine;
using System.Collections;
using System;

public class Weapon_Sword : Weapon {

    public Weapon_Sword()
        : base()
    {

    }

    
    public float attackInterval = 1;
    float m_next;
    bool m_canAttack = true;

    Vector3 hitArea = new Vector3(3,1,3);

    public Weapon_Sword(string name, string desc, int worth) 
        : base(name, desc, worth)
    {
        
    }

    public override void OnEquip()
    {
        Damage = 25;
    }

    void Slash()
    {
        Collider[] hits = Physics.OverlapBox(Owner.Link.transform.position, Vector3.one * 2);/*Physics.OverlapBox(Owner.Link.transform.position + Owner.Link.transform.position, hitArea, Owner.Link.transform.rotation);*/
        foreach (Collider hit in hits)
        {
            VisualCharacter character = hit.GetComponent<VisualCharacter>();
            if (character != null && character != Owner.Link)
            {
                if (character.Link == Owner)
                    Debug.LogFormat("{0}({1}) slashed self", Owner.Link.name, Owner.Health);
                else
                    Debug.LogFormat("{0}({1}) slashed {2}({3})", Owner.Link.name, Owner.Health, character.name, character.Link.Health);

                character.Hurt(Damage);
                break;
            }

        }
    }

    public override void Think()
    {
        Debug.DrawLine(Owner.Link.transform.position, Owner.Link.transform.position + Vector3.forward * hitArea.z);

        if (!m_canAttack)
        {
            m_next -= Time.deltaTime;
            if (m_next <= 0)
            {
                Debug.Log("Can attack");
                m_canAttack = true;
            }
        }
    }

    public override void Attack()
    {
        if (m_canAttack)
        {
            m_next += attackInterval;
            m_canAttack = false;

            Debug.Log("Slash!");
            Slash();

        }
    }

    public override object Clone()
    {
        return MemberwiseClone();
    }
}
