using UnityEngine;
using Helper;

public class ShipControl : MonoBehaviour
{
    public GameObject target;

    private FSMSystem fsm;

    public void SetTransition(Transition t)
    {
        fsm.PerformTransition(t);
    }

    public void Start()
    {
        MakeFSM();
    }

    public void FixedUpdate()
    {
        fsm.CurrentState.TransitionLogic(target, gameObject);
        fsm.CurrentState.BehaviorLogic(target, gameObject);
    }

    private void MakeFSM()
    {
        fsm = new FSMSystem();

        MoveState move = new MoveState();
        //move.AddTransition(Transition.FindTarget, StateID.AttackTarget);
        fsm.AddState(move);
    }
}
