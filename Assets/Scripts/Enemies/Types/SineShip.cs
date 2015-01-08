using UnityEngine;
using FSMHelper;

public class SineShip : Enemy
{
    protected override void MakeFSM()
    {
        fsm = new FSMSystem();

        DisabledState disabled = new DisabledState(gameObject);
        disabled.AddTransition(Transition.Enabled, StateID.SineMoveState);
        fsm.AddState(disabled);

        SineMoveState move = new SineMoveState(gameObject);
        move.AddTransition(Transition.Disabled, StateID.Disabled);
        fsm.AddState(move);
    }
}