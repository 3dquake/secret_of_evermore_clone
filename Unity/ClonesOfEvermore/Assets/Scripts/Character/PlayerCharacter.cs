using UnityEngine;
using System.Collections;

public class PlayerCharacter : VisualCharacter {
    
    Vector3 velocity = Vector3.zero;
    Vector2 direction;

    //Tells the character to either listen for input, or not
    public bool listen;

    void GetInput()
    {
        if (!listen)
            return;

        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        // BUG: this executes twice
        //if (Input.GetButtonUp("Jump"))
        //{
        //    GameManager.Instance.Camera.SetNextTarget();
        //}

    }

    public void Pickup()
    {
        Collider[] items = Physics.OverlapSphere(transform.position + Vector3.down * Controller.height, Controller.radius+10, LayerMask.GetMask("Items"));
        //GameManager.Instance.Inventory.Add(items[i].GetComponent<>);
    }

    public void Drop()
    {

    }

    void Update()
    {
        GetInput();

        Move(direction, 6);
    }

}
