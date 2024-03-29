﻿using UnityEngine;
using FSMHelper;

public class MoveState : FSMState
{
    private Vector2 movement = Vector2.zero;

    public MoveState(GameObject npc)
    {
        stateID = StateID.MoveState;
        SetEnemy(npc);
    }

    public override void TransitionLogic(GameObject target, GameObject npc)
    {
        if (!enemy.isEnabled)
        {
            npc.GetComponentInChildren<Animator>().SetBool("Moving", false);
            enemy.SetTransition(Transition.Disabled);
        }
    }

    public override void BehaviorLogicFixed(GameObject target)
    {
        if (!GameManager.Instance.isPaused)
        {
            enemy.rigidbody2D.velocity = movement;
        }
        else
        {
            enemy.rigidbody2D.velocity = Vector2.zero;
        }
    }

    public override void BehaviorLogic(GameObject target)
    {
        float noise = Random.Range(-1f, 1f);
        movement = new Vector2(enemy.direction.x, enemy.direction.y + noise) * enemy.speed;
        movement = Vector2.ClampMagnitude(movement, enemy.speed);
    }
}