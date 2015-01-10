using UnityEngine;
using FSMHelper;

public class DisabledState : FSMState
{
    public DisabledState(GameObject npc)
    {
        stateID = StateID.Disabled;
        SetEnemy(npc);
    }

    public override void TransitionLogic(GameObject target, GameObject npc)
    {
        if (GameManager.Instance.levelActive)
        {
            npc.GetComponentInChildren<Animator>().SetBool("Moving", true);
            npc.GetComponent<Enemy>().SetTransition(Transition.Enabled);
        }
    }

    public override void BehaviorLogicFixed(GameObject target) { }

    public override void BehaviorLogic(GameObject target) { }
}