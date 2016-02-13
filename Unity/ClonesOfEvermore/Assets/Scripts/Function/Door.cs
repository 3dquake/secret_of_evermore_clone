using UnityEngine;
using System.Collections;

[AddComponentMenu("Function/Door")]
[ExecuteInEditMode]
public class Door : MonoBehaviour
{

    public float openSpeed;
    public float closeSpeed;

    public bool onEnter, onExit, onStay;

    public GameObject m_dummy;
    Vector3 m_startPosition;

    bool m_opening = false;

    public Trigger trigger;

    public TriggerType Type;
    public enum TriggerType
    {
        Open, Close
    }

    public void Open()
    {
        m_opening = true;
    }

    public void Close()
    {
        m_opening = false;
    }

    public void Toggle()
    {
        m_opening = !m_opening;
    }

    void OnEnable()
    {
        if (onEnter)
            trigger.onTriggerEnter += TriggerHandler;

        if (onExit)
            trigger.onTriggerExit += TriggerHandler;

        if (onStay)
            trigger.onTriggerStay += TriggerHandler;

        m_startPosition = transform.position;
    }

    void Update()
    {
        if (!m_dummy)
        {
            m_dummy = new GameObject("Door Open Position");
            m_dummy.transform.SetParent(transform, false);
        }

        if (!Application.isPlaying)
            return;

        m_dummy.transform.SetParent(null, true);

        if (m_dummy)
        {
            if (m_opening && transform.position != m_dummy.transform.position)
                transform.position = Vector3.MoveTowards(transform.position, m_dummy.transform.position, 0.1f);

            if (!m_opening && transform.position != m_startPosition)
                transform.position = Vector3.MoveTowards(transform.position, m_startPosition, 0.1f);

        }
    }

    void OnDrawGizmos()
    {
        if (m_dummy)
        {
            Gizmos.DrawSphere(m_dummy.transform.position, 0.25f);
            Gizmos.DrawLine(transform.position, m_dummy.transform.position);
        }
    }

    void TriggerHandler(Collider collider)
    {
        switch (Type)
        {
            case TriggerType.Open:
                Open();
                break;
            case TriggerType.Close:
                Close();
                break;
            default:
                Close();
                break;
        }
    }

    
}
