using UnityEngine;
using System.Collections;

[AddComponentMenu("Function/Rotate")]
public class Rotate : MonoBehaviour {
    
    public Vector3 rotation;

    void Update()
    {
        transform.Rotate(rotation * Time.deltaTime);
    }

}
