using UnityEngine;
using System.Collections;

public class PlayerCharacter : VisualCharacter {
    
    Vector3 velocity = Vector3.zero;
    Vector2 direction;

    public VisualItem equippedWeapon;
    public VisualItem equippedArmor;

    //Tells the character to either listen for input, or not
    public bool listen;
    public float pickupRadius;
    Vector3 pickupArea;

    void GetInput()
    {
        if (!listen)
            return;

        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (Input.GetButtonDown("Interact"))
        {
            Pickup();
        }

        // BUG: this executes twice
        //if (Input.GetButtonUp("Jump"))
        //{
        //    GameManager.Instance.Camera.SetNextTarget();
        //}

    }

    public void Pickup()
    {
        Collider[] items = Physics.OverlapSphere(transform.position + Vector3.down * Controller.height, pickupRadius, LayerMask.GetMask("Items"));

        if (items.Length == 0)
            return;

        // Take the first occurrence
        VisualItem item = items[0].GetComponent<VisualItem>();

        GameManager.Instance.Inventory.Add(item);
        item.SetActive(false);
    }

    void Update()
    {
        listen = (GameManager.Instance.Characters.Selected == Link);
        pickupArea = transform.position + Vector3.down * Controller.height;

        GetInput();

        Move(direction, 6);
    }

    void OnDrawGizmos()
    {
        pickupArea = transform.position + Vector3.down * Controller.height;
        Gizmos.DrawWireSphere(pickupArea, pickupRadius);
    }

}
