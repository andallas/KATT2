using UnityEngine;
using System.Collections.Generic;
using Helper;

public abstract class FSMState
{
    public StateID ID { get { return stateID; } }

    protected Dictionary<Transition, StateID> map = new Dictionary<Transition, StateID>();
    protected StateID stateID;


    public void AddTransition(Transition trans, StateID id)
    {
        string errorMsg = "";
        if (trans == Transition.NullTransition)
        {
            errorMsg = "NullTransition is not allowed for a real transition.";
            FSMStateError(errorMsg);
            return;
        }

        if (id == StateID.NullStateID)
        {
            errorMsg = "NullStateID is not allowed for a real ID.";
            FSMStateError(errorMsg);
            return;
        }

        if (map.ContainsKey(trans))
        {
            errorMsg = "State " + stateID.ToString() + " already has a transition " + trans.ToString() + ". Impossible to assign to another state.";
            FSMStateError(errorMsg);
            return;
        }

        map.Add(trans, id);
    }

    public void DeleteTransition(Transition trans)
    {
        string errorMsg = "";
        if (trans == Transition.NullTransition)
        {
            errorMsg = "NullTransition is not allowed.";
            FSMStateError(errorMsg);
            return;
        }

        if (map.ContainsKey(trans))
        {
            map.Remove(trans);
            return;
        }

        errorMsg = "Transition " + trans.ToString() + " passed to " + stateID.ToString() + " was not on the state's transition list.";
        FSMStateError(errorMsg);
    }

    public StateID GetOutputState(Transition trans)
    {
        if (map.ContainsKey(trans))
        {
            return map[trans];
        }
        return StateID.NullStateID;
    }

    public virtual void DoBeforeEntering() { }

    public virtual void DoBeforeLeaving() { }

    public abstract void TransitionLogic(GameObject target, GameObject npc);

    public abstract void BehaviorLogic(GameObject target, GameObject npc);

    private void FSMStateError(string errorMsg)
    {
        Debug.LogError("FSMState Error: " + errorMsg);
    }
}