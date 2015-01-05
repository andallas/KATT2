using UnityEngine;
using FSMHelper;

public class Popcorn : Enemy
{
    protected override void MakeFSM()
    {
        fsm = new FSMSystem();

        DisabledState disabled = new DisabledState(gameObject);
        disabled.AddTransition(Transition.Enabled, StateID.MoveState);
        fsm.AddState(disabled);

        MoveState move = new MoveState(gameObject);
        move.AddTransition(Transition.Disabled, StateID.Disabled);
        fsm.AddState(move);
    }
}