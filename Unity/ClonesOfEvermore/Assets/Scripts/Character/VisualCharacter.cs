using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class VisualCharacter : MonoBehaviour {

    /// <summary>
    /// Property to access the CharacterController component
    /// </summary>
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

    /// <summary>
    /// Link to the character data.
    /// This will be linked when 'CharacterManager' finds and links all the characters
    /// </summary>
    public Character Link
    {
        get
        {
            return m_link;
        }
        set
        {
            if (m_link == null)
            {
                m_link = value;
                m_link.Link = this;
            }
        }
    }
    Character m_link;

    public delegate void CharacterEvent();
    public event CharacterEvent onDeath;

    [Header("Starting values")]
    public string charName;
    [Range(0,100)]
    public int health, mana, level;
    [Range(1, 10)]
    public int attack, defence, agility;

    public bool isDead
    {
        get;
        set;
    }

    Vector3 m_gravity = new Vector3(0, 9.81f, 0);

    [Header("Other")]
    [Range(1f, 100f)]
    public float baseSpeed;

    [Range(.01f, 1f)]
    public float moveLerp, rotLerp;

    Vector3 m_velocity = Vector3.zero;
    Vector3 m_targetVelocity = Vector3.zero;
    Quaternion m_lookRotation = Quaternion.identity;

    /// <summary>
    /// Top of the CharacterController
    /// </summary>
    public Vector3 head
    {
        get
        {
            return transform.position + Vector3.up * Controller.height;
        }
    }
    /// <summary>
    /// Bottom of the CharacterController
    /// </summary>
    public Vector3 feet
    {
        get
        {
            return transform.position + Vector3.down * Controller.height;
        }
    }

    /// <summary>
    /// Relative (0,0,1)
    /// </summary>
    public Vector3 forward
    {
        get
        {
            return transform.position + Vector3.forward;
        }
    }
    /// <summary>
    /// Relative (0,1,0)
    /// </summary>
    public Vector3 up
    {
        get
        {
            return transform.position + Vector3.up;
        }
    }
    /// <summary>
    /// Relative (1,0,0)
    /// </summary>
    public Vector3 right
    {
        get
        {
            return transform.position + Vector3.right;
        }
    }

    void DropToFloor()
    {
        Controller.Move(Vector3.down * 50f);

        //RaycastHit hit;
        //Physics.Raycast(transform.position, Vector3.down, out hit, 50f);
        //transform.position = hit.point + Vector3.up * Controller.height;
    }

    void OnEnable () {
	    DropToFloor();
	}
    
    public void Move(Vector2 direction)
    {
        //// Check if we need to move
        //if (direction == Vector2.zero)
        //    return;

        // Reorder Vector2 components XY to Vector3 XZ
        Vector3 reordered = new Vector3(direction.x, 0, direction.y);

        // Set target velocity
        m_targetVelocity = (reordered * (baseSpeed * (1 + Link.Agility / 20))) - m_gravity;

        // Apply to our velocity
    }

    public void Look(Vector3 direction)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotLerp);
    }

    void Update()
    {
        Think();

        if (Link.Health <= 0 || transform.position.z < GameManager.Instance.KillZ && !isDead)
        {
            Kill();
        }

        m_velocity = Vector3.Lerp(m_velocity, m_targetVelocity * Time.deltaTime, moveLerp);

        if (m_velocity != Vector3.zero)
        {
            // Move our character, if needed
            Controller.Move(m_velocity);
        }
    }

    public void Attack()
    {
        Link.Weapon.Attack();
    }
    
    public void Heal(int amount)
    {
        Link.Health += amount;
    }

    public void Hurt(int amount)
    {
        Link.Health -= amount;
    }

    /// <summary>
    /// Kills this character
    /// </summary>
    public void Kill()
    {
        if (onDeath != null)
            onDeath();
        isDead = true;
        Link.Health = 0;
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Resurrects this character from the dead
    /// </summary>
    public void Resurrect()
    {

    }

    /// <summary>
    /// Seperate Update function
    /// </summary>
    protected virtual void Think()
    {
        
    }



}
