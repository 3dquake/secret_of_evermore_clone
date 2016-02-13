using System;
using System.Collections.Generic;

using UnityEngine;

using StateMachines;

public abstract class CameraState : State
{
    public CameraState()
    {
        target = GameManager.Instance.Camera.currentTarget;
        camera = GameManager.Instance.Camera.activeCamera;
    }

    public GameObject target { get; set; }
    public Camera camera { get; private set; }
}

/// <summary>
/// Camera functionality
/// </summary>
public class CameraController : StateMachine<CameraState>
{
    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="characters">Array of targets to follow</param>
    public CameraController(params GameObject[] _targets)
    {
        m_mainCamera = Camera.main;
        if (!activeCamera)
            activeCamera = m_mainCamera;
        if (!activeCamera)
            Debug.LogError("No MainCamera in scene!");
        m_targetList = new List<GameObject>(_targets.Length);
        m_targetList.AddRange(_targets);
        m_enumerator = m_targetList.GetEnumerator();
        m_enumerator.MoveNext();

        ChangeTarget(m_targetList[0]);
    }

    // <summary>
    // Camera smoothing value. Clamped between 0.0 and 1.0
    // </summary>
    //public float Smoothing
    //{
    //    get
    //    {
    //        return m_lerp;
    //    }
    //    set
    //    {
    //        m_lerp = Mathf.Clamp(m_lerp, 0f, 1f);
    //    }
    //}
    //public float m_lerp;
    //public float Distance { get; set; }

    /// <summary>
    /// Current target we are following
    /// </summary>
    public GameObject currentTarget
    {
        get;
        set;
    }
    VisualCharacter m_targetComponent;

    List<GameObject>.Enumerator m_enumerator;
    List<GameObject> m_targetList;
    //int m_index = 0;

    Camera m_mainCamera;
    /// <summary>
    /// Currently active camera to control
    /// </summary>
    public Camera activeCamera
    {
        get;
        set;
    }

    /// <summary>
    /// Change target to specified gameObject
    /// </summary>
    /// <param name="target">GameObject to set target to</param>
    public void ChangeTarget(GameObject target)
    {
        if (m_targetList.Contains(target))
        {
            currentTarget = target;
            GameManager.Instance.Characters.Selected = currentTarget.GetComponent<VisualCharacter>().Link;
        }
    }

    /// <summary>
    /// Change target to specified gameObject name
    /// </summary>
    /// <param name="name">GameObject name</param>
    public void ChangeTarget(string name)
    {
        GameObject temp = m_targetList.Find(x => x.name == name);
        if (!temp)
        {
            Debug.LogWarningFormat("Target '{0}' was not found", name);
            return;
        }
        ChangeTarget(temp);
    }

    /// <summary>
    /// Selects the next target in the list
    /// </summary>
    public void NextTarget()
    {
        if (m_enumerator.MoveNext())
            ChangeTarget(m_enumerator.Current);
        else
        {
            m_enumerator = m_targetList.GetEnumerator();
            m_enumerator.MoveNext();
            ChangeTarget(m_enumerator.Current);
        }
    }

    /// <summary>
    /// Update the camera systems
    /// </summary>
    public void Update()
    {
        VisualCharacter character = currentTarget.GetComponent<VisualCharacter>();

        if (m_targetComponent != character)
            m_targetComponent = character;

        if (GameManager.Instance.Characters.Selected != m_targetComponent.Link)
            GameManager.Instance.Characters.Selected = m_targetComponent.Link;

        UpdateState();
    }

    //void ChangeTarget(int index)
    //{
    //    //if (Targets[index] == null)
    //    //    return;

    //    // Tell CharManager that we have a new selection
    //    GameManager.Instance.Characters.Selected = targets[index].Link;
    //    Current = targets[index].transform;
    //}

    //public void NextTarget()
    //{
    //    m_index++;

    //    //m_index = m_index % Targets.Length-1;

    //    if (m_index > targets.Length - 1)
    //        m_index = 0;

    //    ChangeTarget(m_index);
    //}

    //public void Initialize()
    //{
    //    if (activeCamera == null)
    //        return;

    //    //Save this value ...
    //    m_initialRotation = activeCamera.transform.rotation;

    //    if (currentTarget == null)
    //        ChangeTarget(0);
    //}

    ////void Follow()
    ////{
    ////    if (!currentTarget)
    ////        return;
    ////    activeCamera.transform.position = Vector3.Lerp(activeCamera.transform.position, currentTarget.position + m_constPosition/* * Distance*/, m_lerp);
    ////}

    //void Update()
    //{

    //    //if (Target != GameManager.Instance.Characters.Selected.Link.transform && GameManager.Instance.Characters.Selected != null)
    //    //    Target = GameManager.Instance.Characters.Selected.Link.transform;

    //    // So we can lock the camera in it's initial position
    //    if (activeCamera.transform.rotation != m_initialRotation)
    //        activeCamera.transform.rotation = m_initialRotation;

    //    //Follow();
    //}

}

