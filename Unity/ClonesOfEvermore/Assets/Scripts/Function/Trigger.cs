using UnityEngine;
using System.Collections;

[AddComponentMenu("Function/Trigger")]
[RequireComponent(typeof(Collider))]
public class Trigger : MonoBehaviour {
   
    public delegate void TriggerEvent(Collider collider);
    public event TriggerEvent onTriggerStay;
    public event TriggerEvent onTriggerExit;
    public event TriggerEvent onTriggerEnter;

    public Collider trigger
    {
        get
        {
            if (!m_trigger)
                m_trigger = GetComponent<Collider>();
            if (!m_trigger.isTrigger)
                m_trigger.isTrigger = true;
            return m_trigger;
        }
    }
    Collider m_trigger;

    public string[] filter;

    bool Filter(Collider other)
    {
        foreach (string tag in filter)
        {
            return (other.tag == tag);
        }
        return false;
    }

    public void OnTriggerStay(Collider other)
    {
        Debug.Log("STAY:"+other.name);
        if (onTriggerStay != null && Filter(other))
            onTriggerStay(other);
    }

    public void OnTriggerExit(Collider other)
    {
        Debug.Log("EXIT:"+other.name);
        if (onTriggerExit != null && Filter(other))
            onTriggerExit(other);
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("ENTER:"+other.name);
        if (onTriggerEnter != null && Filter(other))
            onTriggerEnter(other);
    }

}
