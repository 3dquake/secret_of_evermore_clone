using UnityEngine;
using System.Collections;

public class AICharacter : VisualCharacter {

    public AIBehaviour Behaviour
    {
        get
        {
            return m_behaviour;
        }
    }
    AIBehaviour m_behaviour;
    


}
