using UnityEngine;
using System.Collections;

[AddComponentMenu("Function/Game End")]
public class GameEnd : MonoBehaviour {
    
    public Trigger trigger;

    public bool onEnter, onExit, onStay;

    void OnEnable()
    {
        if (onEnter)
            trigger.onTriggerEnter += TriggerHandler;

        if (onExit)
            trigger.onTriggerExit += TriggerHandler;

        if (onStay)
            trigger.onTriggerStay += TriggerHandler;
    }

    void TriggerHandler(Collider collider)
    {
        Debug.Log("END");
        GameManager.Instance.ChangeState("GameStateEnd");
    }

}
