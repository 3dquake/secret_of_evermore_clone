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

    void DropToFloor()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.down, out hit, 50f);

        transform.position = hit.point + Vector3.up * Controller.height;
        Controller.Move(Vector3.zero);
    }

    // Use this for initialization
    void OnEnable () {
	    DropToFloor();
	}

    public void Move(Vector2 dir)
    {
        //Controller.Move();
    }
    
}
