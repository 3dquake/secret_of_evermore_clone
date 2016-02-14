using System;
using UnityEngine;

public class Weapon_Bow : Weapon
{
    public GameObject projectile;

    float m_next;
    public float interval = 1f; 
    bool m_canShoot = true;

    public Weapon_Bow()
        : base()
    {

    }

    public override void OnEquip()
    {
        if (!projectile)
        {
            projectile = Resources.Load<GameObject>("Prefabs/Projectiles/Arrow");
            projectile.SetActive(false);
        }
    }

    public Weapon_Bow(string name, string desc, int worth) : base(name, desc, worth)
    {
        
    }

    public override void Think()
    {
        if (!m_canShoot)
        {
            m_next -= Time.deltaTime;
            if (m_next <= 0)
                m_canShoot = true;
        }
    }

    void Shoot()
    {
        GameObject clone = GameObject.Instantiate<GameObject>(projectile);
        Vector3 position = Owner.Link.transform.position + Owner.Link.transform.forward;
        Quaternion rotation = Quaternion.LookRotation(Owner.Link.transform.forward);

        clone.transform.position = position;
        clone.transform.rotation = rotation;
        clone.SetActive(true);

    }

    public override void Attack()
    {
        if (m_canShoot)
        {
            Shoot();
            m_canShoot = false;
            m_next += interval;
        }
    }

    public override object Clone()
    {
        return MemberwiseClone();
    }
}