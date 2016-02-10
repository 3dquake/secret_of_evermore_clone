using UnityEngine;
using System.Collections;

public class PlayerCharacter : VisualCharacter {
    
    Vector3 velocity = Vector3.zero;

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector2 input = new Vector2(x,y);

        velocity = Vector3.Lerp(velocity, new Vector3(input.x, 0, input.y).normalized * 6 * Time.deltaTime, 0.25f);

        Controller.Move(velocity);
    }

}
