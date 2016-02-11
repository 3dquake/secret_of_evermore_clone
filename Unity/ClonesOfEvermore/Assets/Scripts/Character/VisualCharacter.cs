using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class VisualCharacter : MonoBehaviour {

    public CharacterController Controller
    {
        get
        {
            if (!m_controller)
                m_controller = GetComponent<CharacterController>();

            return m_controller;
        }
    }
    CharacterController m_controller;

    public int startingHealth, startingMana;

    // This will be later affected by agility
    [Range(1f, 100f)]
    public float moveSpeed;

    [Range(.01f, 1f)]
    public float moveLerp, rotLerp;

    Vector3 m_velocity = Vector3.zero;
    Vector3 m_targetVelocity = Vector3.zero;

    void DropToFloor()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.down, out hit, 50f);

        transform.position = hit.point + Vector3.up * Controller.height;
    }

    void OnEnable () {
	    DropToFloor();
	}
    
    public void Move(Vector2 direction, float speed)
    {
        // Check if we need to move
        if (direction == Vector2.zero || speed <= 0)
            return;

        // Reorder Vector2 components XY to Vector3 XZ
        Vector3 reordered = new Vector3(direction.x, 0, direction.y);

        // Set target velocity
        m_targetVelocity = reordered * speed;

        // Apply to our velocity
        m_velocity = Vector3.Lerp(m_velocity, m_targetVelocity * Time.deltaTime, moveLerp);
        if (m_velocity != Vector3.zero)
        {
            // And move our character, if needed
            Controller.Move(m_velocity);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(m_velocity.normalized), rotLerp);
        }

    }

    public void Attack()
    {
        
    }
    
}
