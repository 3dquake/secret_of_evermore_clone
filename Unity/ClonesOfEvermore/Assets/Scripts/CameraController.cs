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

    void ChangeTarget(int index)
    {
        //if (Targets[index] == null)
        //    return;

        // Tell CharManager that we have a new selection
        GameManager.Instance.Characters.Selected = Targets[index].Link;
        Current = Targets[index].transform;
    }

    public void SetNextTarget()
    {
        m_index++;

        //m_index = m_index % Targets.Length-1;

        if (m_index > Targets.Length - 1)
            m_index = 0;

        ChangeTarget(m_index);
    }

    public void Initialize()
    {

        //Save this value ...
        m_initialRotation = transform.rotation;

        if (Current == null)
            ChangeTarget(0);
    }
    
    void Follow()
    {
        if (!Current)
            return;
        transform.position = Vector3.Lerp(transform.position, Current.position + m_constPosition * distance, lerp);
    }

    void Update()
    {

        //if (Target != GameManager.Instance.Characters.Selected.Link.transform && GameManager.Instance.Characters.Selected != null)
        //    Target = GameManager.Instance.Characters.Selected.Link.transform;

        // So we can lock the camera in it's initial position
        if (transform.rotation != m_initialRotation)
            transform.rotation = m_initialRotation;

        Follow();
    }

}

