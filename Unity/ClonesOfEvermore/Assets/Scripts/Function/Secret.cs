using UnityEngine;
using System.Collections;

[AddComponentMenu("Function/Secret")]
public class Secret : MonoBehaviour
{

    public GameObject target;
    public Trigger trigger;

    public bool onEnter, onExit, onStay;

    void OnEnable()
    {
        target.SetActive(false);

        if (onEnter)
            trigger.onTriggerEnter += TriggerHandler;

        if (onExit)
            trigger.onTriggerExit += TriggerHandler;

        if (onStay)
            trigger.onTriggerStay += TriggerHandler;

    }

    void TriggerHandler(Collider collider)
    {
        PlayerCharacter player = collider.GetComponent<PlayerCharacter>();
        if (!player)
            return;

        if (player.listen && player.name == "Character.Dog")
            target.SetActive(true);
    }

}
