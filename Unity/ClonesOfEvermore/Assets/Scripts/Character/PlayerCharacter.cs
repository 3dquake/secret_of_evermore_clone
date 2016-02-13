using UnityEngine;
using System.Collections;

using StateMachines.AI;
using StateMachines.AI.States;

public class PlayerCharacter : VisualCharacter {
    
    AIStateMachine Behaviour;

    Vector3 m_direction; // Always normalized

    [Header("AI settings")]
    public GameObject followTarget;
    public float maxFollowRange;
    public float minFollowRange;

    public Vector3 pickupArea
    {
        get
        {
            return transform.position + Vector3.down * Controller.height;
        }
    }

    //Tells the character to either listen for input, or not
    public bool listen;
    public float pickupRadius;
    //Vector3 m_pickupArea;
        
    public void ChangeCharacter()
    {
        GameManager.Instance.Camera.NextTarget();
    }

    void GetInput()
    {
        m_direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

        if (Input.GetButtonDown("Interact"))
        {
            Pickup();
        }

        if (Link.Weapon != null && Input.GetButtonDown("Fire1"))
        {
            Link.Weapon.Attack();
        }

        // BUG: this executes twice?
        //if (Input.GetButtonUp("Jump"))
        //{
        //    GameManager.Instance.Camera.SetNextTarget();
        //}

    }

    /// <summary>
    /// Picks up an item
    /// </summary>
    public void Pickup()
    {
        Collider[] items = Physics.OverlapSphere(pickupArea, pickupRadius, LayerMask.GetMask("Items"));

        if (items.Length == 0)
            return;

        // Take the first occurrence
        VisualItem item = items[0].GetComponent<VisualItem>();

        // Doesn't have a VisualItem component; abort
        if (item == null)
            return;

        // If we can add it, we disable it's visual link
        if (GameManager.Instance.Inventory.Add(item))
            item.Active = false;
    }

    protected override void Begin()
    {
        Behaviour = new AIStateMachine(this);
        AIStateCompanion state = new AIStateCompanion(this);

        state.target = followTarget;
        state.minRange = minFollowRange;
        state.maxRange = maxFollowRange;

        Behaviour.AddState(state);
        Behaviour.ChangeState(state);
    }
    protected override void Think()
    {
        listen = (GameManager.Instance.Characters.Selected == Link);
        

        //if (!listen)
        //{
        //    if (m_direction != Vector3.zero)
        //        m_direction = Vector3.zero;
        //    return;
        //}

        if (!listen)
        {

            Behaviour.UpdateState();
        }
        else
        {
            GetInput();
            Move(m_direction);
            base.Think();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pickupArea, pickupRadius);
    }

}
