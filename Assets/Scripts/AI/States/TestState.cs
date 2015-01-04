using UnityEngine;
using FSMHelper;

public class TestState : FSMState
{
    public TestState(GameObject npc)
    {
        stateID = StateID.MoveState;
        SetEnemy(npc);
    }

    public override void TransitionLogic(GameObject target, GameObject npc)
    {
        /*
            if(npc.GetComponent<Enemy>().GetTargetPlanet() != null)
            {
                npc.GetComponent<Enemy>().SetTransition(Transition.KnownPlanet);
            }
            else
            {
                npc.GetComponent<Enemy>().SetTransition(Transition.NoPlanet);
            }
        */
        throw new System.NotImplementedException();
    }

    public override void BehaviorLogicFixed(GameObject target)
    {
        throw new System.NotImplementedException();
    }

    public override void BehaviorLogic(GameObject target)
    {
        throw new System.NotImplementedException();
    }
}