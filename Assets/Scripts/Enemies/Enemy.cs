using UnityEngine;
using FSMHelper;

public abstract class Enemy : MonoBehaviour
{
    public int scoreValue;
    public GameObject target { get; set; }
    public float speed = 3f;
    public Vector2 direction = new Vector2(-1, 0);
    public bool isEnabled { get { return _isEnabled; } }

    protected GameObject _target;
    protected bool _isEnabled = false;
    protected FSMSystem fsm;
    protected Weapon[] weapons;
    protected bool hasSpawn;

    void Awake()
    {
        MakeFSM();
        weapons = GetComponents<Weapon>();
        Enable(false);
    }

    public void FixedUpdate()
    {
        fsm.CurrentState.BehaviorLogicFixed(target);
    }

    void Update()
    {
        fsm.CurrentState.TransitionLogic(target, gameObject);
        fsm.CurrentState.BehaviorLogic(target);

        if (!hasSpawn)
        {
            if (renderer.IsVisibleFrom(Camera.main))
            {
                Enable(true);
            }
        }
        else
        {
            if(GameManager.Instance.levelActive && !GameManager.Instance.isPaused)
            {
                foreach (Weapon weapon in weapons)
                {
                    if (weapon != null)
                    {
                        weapon.Attack(true);
                    }
                }
            }

            if (!renderer.IsVisibleFrom(Camera.main))
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetTransition(Transition t)
    {
        fsm.PerformTransition(t);
    }

    protected void Enable(bool enable)
    {
        _isEnabled = enable;
        hasSpawn = enable;
        collider2D.enabled = enable;

        foreach (Weapon weapon in weapons)
        {
            weapon.enabled = enable;
        }
    }

    protected abstract void MakeFSM();
}