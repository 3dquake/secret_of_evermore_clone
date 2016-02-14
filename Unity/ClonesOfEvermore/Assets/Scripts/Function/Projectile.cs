using UnityEngine;
using System.Collections;

[AddComponentMenu("Function/Projectile")]
[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    public Rigidbody Rigidbody
    {
        get
        {
            if (!m_rigidbody)
                m_rigidbody = GetComponent<Rigidbody>();
            return m_rigidbody;
        }
    }
    Rigidbody m_rigidbody;

    public int damage;
    public int speed;

    public void Move()
    {
        Rigidbody.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        VisualCharacter character = collision.gameObject.GetComponent<VisualCharacter>();
        if (character)
        {
            character.Hurt(damage);
        }
        Destroy(gameObject);
    }

    void Update()
    {
        Move();
    }


}
