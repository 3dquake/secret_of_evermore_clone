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

        if (Input.GetButtonUp("Jump"))
        {
            GameManager.Instance.Camera.SetNextTarget();
        }

    }

    void Update()
    {
        GetInput();

        Move(direction, 6);
    }

}
