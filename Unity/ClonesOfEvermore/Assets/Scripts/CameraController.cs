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

    public VisualCharacter[] Targets;
    public Transform Current;

    Quaternion m_initialRotation;
    Vector3 m_constPosition = new Vector3(0,10,-10);

    int m_index = 0;

    public void SetNextTarget()
    {
        m_index++;

        if (m_index > Targets.Length - 1)
            m_index = 0;

        Current = Targets[m_index].transform;
    }

    void Awake()
    {
        //Save this value ...
        m_initialRotation = transform.rotation;

        if (Current == null)
            Current = Targets[0].transform;
    }

    void Update()
    {
        //if (Target != GameManager.Instance.Characters.Selected.Link.transform && GameManager.Instance.Characters.Selected != null)
        //    Target = GameManager.Instance.Characters.Selected.Link.transform;

        // So we can lock the camera in it's initial position
        if (transform.rotation != m_initialRotation)
            transform.rotation = m_initialRotation;

        transform.position = Vector3.Lerp(transform.position, Current.position + m_constPosition * distance, lerp);

    }

}

