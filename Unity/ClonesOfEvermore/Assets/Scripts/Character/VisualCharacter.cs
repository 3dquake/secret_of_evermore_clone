using UnityEngine;
using System.Collections;

/// <summary>
/// Visual presentation of characters. Don't use MonoBehaviour, use virtual methods
/// </summary>
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

    /// <summary>
    /// This event is raised upon the death of the character
    /// </summary>
    public event CharacterEvent onDeath;

    [Header("Starting values")]
    public string charName;
    [Range(0,100)]
    public int health, mana, level;
    [Range(1, 10)]
    public int attack, defence, agility;
    
    public VisualItem weapon;
    public VisualItem armor;

    /// <summary>
    /// Is this character dead?
    /// </summary>
    public bool isDead
    {
        get;
        set;
    }

    [Header("Other")]
    [Range(1f, 100f)]
    public float baseSpeed;

    [Range(.01f, 1f)]
    public float moveLerp, rotLerp;

    // Current velocity
    Vector3 m_velocity = Vector3.zero;
    Vector3 m_targetVelocity = Vector3.zero;
    Vector3 m_push = Vector3.zero;
    Quaternion m_lookRotation = Quaternion.identity;
    Vector3 m_gravity = new Vector3(0, 9.81f, 0);

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

    // Simply insta-drops any floaters
    void DropToFloor()
    {
        Controller.Move(Vector3.down * 50f);
    }

    void OnEnable () {
	    DropToFloor();
        Begin();
	}

    void Update()
    {
        if (!gameObject.activeSelf)
            return;

        // Update possibly equipped weapon
        if (Link.Weapon != null)
            Link.Weapon.Think();

        // Update character
        Think();

        // Set velocity
        m_velocity = Vector3.Lerp(m_velocity, ((m_targetVelocity + m_push) - m_gravity) * Time.deltaTime, moveLerp);
        m_targetVelocity = m_push = Vector3.zero; // Reset target velocity. Prevents "onpressing" and stops AI

        if (m_velocity != Vector3.zero)
        {
            // Move our character, if needed
            Controller.Move(m_velocity);
        }

        // Check if character is dead or below KillY
        if (Link.Health <= 0 || transform.position.y < GameManager.Instance.KillHeight && !isDead)
        {
            Kill();
        }
    }

    /// <summary>
    /// Moves to the designated point
    /// </summary>
    /// <param name="point">Point to go to</param>
    /// <param name="radius">Radius of when to stop following</param>
    public void Follow(Vector3 point, float radius)
    {
        float distance = Vector3.Distance(transform.position, point);
        if (distance > radius)
            Move((point - transform.position).normalized);
    }

    /// <summary>
    /// Searches the surrounding area for 'tag'
    /// </summary>
    /// <param name="position">Position of the search</param>
    /// <param name="radius">Radius of the search</param>
    /// <param name="tag">Tag to search for</param>
    /// <returns>VisualCharacter if found</returns>
    public VisualCharacter Search(Vector3 position, float radius, string tag)
    {
        VisualCharacter result = null;
        foreach (Collider target in Physics.OverlapSphere(transform.position, radius))
        {
            if (target.tag == tag)
            {
                result = target.GetComponent<VisualCharacter>();
            }
        }
        return result;
    }

    /// <summary>
    /// Basic move method. Moves the character in specified direction
    /// </summary>
    /// <param name="direction">Direction to go to</param>
    public void Move(Vector3 direction)
    {

        // Reorder Vector2 components XY to Vector3 XZ
        //Vector3 reordered = new Vector3(direction.x, 0, direction.y);

        Look(direction.normalized);

        // Set target velocity
        m_targetVelocity = (direction * (baseSpeed * (1 + Link.Agility / 20)));

    }

    /// <summary>
    /// Look at specified direction
    /// </summary>
    /// <param name="direction">Direction to look at</param>
    public void Look(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            Vector3 reordered = new Vector3(direction.x, 0, direction.z).normalized;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(reordered), rotLerp);
        }
    }

    /// <summary>
    /// Make this charater attack with the current weapon (if available)
    /// </summary>
    public void Attack()
    {
        if (Link.Weapon != null)
            Link.Weapon.Attack();
    }
    
    /// <summary>
    /// Heal this character with specified amount
    /// </summary>
    /// <param name="amount">Amount of health to add</param>
    public void Heal(int amount)
    {
        Link.Health += amount;
    }

    /// <summary>
    /// Hurt this character with specified amount
    /// </summary>
    /// <param name="amount">Amount of damage</param>
    public void Hurt(int amount)
    {
        Link.Health -= (amount - Link.Defence);
        //m_push = (transform.forward + transform.up * -10).normalized * -amount;
    }

    /// <summary>
    /// Kill this character
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
    /// Character update method
    /// </summary>
    protected virtual void Think() { }
    /// <summary>
    /// Character initialization method
    /// </summary>
    protected virtual void Begin() { }
    
    void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, m_velocity.normalized * (m_velocity.magnitude * 10));
    }

}
