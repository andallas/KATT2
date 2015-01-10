using UnityEngine;
using FSMHelper;

public class SineMoveState : FSMState
{
    private Vector2 movement = Vector2.zero;

    public SineMoveState(GameObject npc)
    {
        stateID = StateID.SineMoveState;
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
            Vector3 screenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
            Vector3 pos = new Vector3(enemy.transform.position.x, Mathf.Clamp(enemy.transform.position.y, -screenSize.y + 0.1f, screenSize.y - 0.5f), enemy.transform.position.z);
            enemy.transform.position = pos;
        }
        else
        {
            enemy.rigidbody2D.velocity = Vector2.zero;
        }
    }

    public override void BehaviorLogic(GameObject target)
    {
        float noise = Random.Range(-0.25f, 0.25f);
        float y = Mathf.Sin((enemy.transform.position.x - Camera.main.transform.position.x)) * 2;
        movement = new Vector2(enemy.direction.x, y + noise) * enemy.speed;
        movement = Vector2.ClampMagnitude(movement, enemy.speed);
    }
}