using UnityEngine;
using Helper;

public class MoveState : FSMState
{
    public MoveState()
    {
        stateID = StateID.MoveState;
    }

    public override void TransitionLogic(GameObject target, GameObject npc)
    {
        /*
            if(npc.GetComponent<ShipControl>().GetTargetPlanet() != null)
            {
                npc.GetComponent<ShipControl>().SetTransition(Transition.KnownPlanet);
            }
            else
            {
                npc.GetComponent<ShipControl>().SetTransition(Transition.NoPlanet);
            }
        */
        throw new System.NotImplementedException();
    }

    public override void BehaviorLogic(GameObject target, GameObject npc)
    {
        throw new System.NotImplementedException();
    }
}