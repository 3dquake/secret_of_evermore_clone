using System;

using UnityEngine;

/// <summary>
/// Camera functionality
/// </summary>
[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{

    public float lerp;
    public float distance;

    /// <summary>
    /// Target to follow
    /// </summary>
    public Vector3 Target
    {
        get
        {
            return m_target;
        }
        set
        {
            m_target = value;
            OnChanged();
        }
    }
    Vector3 m_target;

    Quaternion initialRotation;

    void Awake()
    {
        //Save this value ...
        initialRotation = transform.rotation;
    }

    void Update()
    {
        // So we can lock the camera in it's initial position
        if (transform.rotation != initialRotation)
            transform.rotation = initialRotation;

        transform.position = 
        
    }

    void OnChanged()
    {

    }

}

