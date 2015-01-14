using UnityEngine;
using System.Collections;
using Helper;

using System;

public class Player : MonoBehaviour
{
    public float speed;
    public GameObject invulnerableEffect;

    private Vector2 movement;
    private Transform _transform;
    private float halfWidth, halfHeight;
    private GameObject _invulnerableEffect;
    private Animator animator;

    void Awake()
    {
        _invulnerableEffect = (GameObject)Instantiate(invulnerableEffect);
        _invulnerableEffect.transform.position = this.transform.position;
        _invulnerableEffect.transform.SetParent(this.transform);
        _invulnerableEffect.SetActive(false);
        _transform = transform;
        halfWidth = renderer.bounds.size.x / 2;
        halfHeight = renderer.bounds.size.y / 2;
        animator = GetComponentInChildren<Animator>();
    }

    void OnEnable()
    {
        int upgradesCount = GameManager.Instance.upgrades.Count;
        for (int i = 0; i < upgradesCount; i++)
        {
            Item item = GameManager.Instance.upgrades[i];
            switch(item.title)
            {
                case "Shield":
                    Shield(item.level);
                    break;
                case "Fire Rate":
                    FireRate(item.level);
                    break;
                case "Energy":
                    Energy(item.level);
                    break;
                case "Speed":
                    Speed(item.level);
                    break;
                case "Drone":
                    Drone(item.level);
                    break;
                default: break;
            }
        }
    }

    void Update()
    {
        if (GameManager.Instance.levelActive && !GameManager.Instance.isPaused)
        {
            float inputX = Input.GetAxis("Horizontal");
            float inputY = Input.GetAxis("Vertical");

            if(inputX != 0 || inputY != 0)
            {
                animator.SetBool("Moving", true);
            }
            else
            {
                animator.SetBool("Moving", false);
            }

            movement = new Vector2(inputX, inputY) * speed;
            movement = Vector2.ClampMagnitude(movement, speed);

            bool shoot = Input.GetButton("Fire1");
            shoot |= Input.GetButton("Fire2");

            if (shoot)
            {
                Weapon weapon = GetComponent<Weapon>();
                if (weapon != null && weapon.CanAttack)
                {
                    weapon.Attack(false);
                }
            }

            Camera mainCamera = Camera.main;

            float distance = (_transform.position - mainCamera.transform.position).z;
            float leftBorder = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, distance)).x;
            float rightBorder = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, distance)).x;
            float topBorder = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, distance)).y;
            float bottomBorder = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, distance)).y;

            _transform.position = new Vector3(Mathf.Clamp(_transform.position.x, leftBorder + halfWidth, rightBorder - halfWidth),
                                             Mathf.Clamp(_transform.position.y, topBorder + halfHeight, bottomBorder - halfHeight),
                                             _transform.position.z);
        }
    }

    void FixedUpdate()
    {
        if (GameManager.Instance.levelActive && !GameManager.Instance.isPaused)
        {
            rigidbody2D.velocity = movement;
        }
        else
        {
            rigidbody2D.velocity = Vector3.zero;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        bool damagePlayer = false;

        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            Health enemyHealth = enemy.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.Damage(enemyHealth.healthPoints);
            }
            damagePlayer = true;
        }

        if (damagePlayer)
        {
            Health playerHealth = this.GetComponent<Health>();
            if(playerHealth != null)
            {
                playerHealth.Damage(1);
            }
        }
    }

    public void DeathSequence()
    {
        Renderer[] renderers = this.GetComponentsInChildren<Renderer>();
        int rendLength = renderers.Length;
        for (int i = 0; i < rendLength; i++)
        {
            renderers[i].enabled = false;
        }

        Collider2D[] colliders = this.GetComponentsInChildren<Collider2D>();
        int colLength = colliders.Length;
        for (int j = 0; j < colLength; j++)
        {
            colliders[j].enabled = false;
        }
    }

    public void Invulnerable(float seconds)
    {
        this.GetComponent<Health>().Invulnerable(true);
        _invulnerableEffect.SetActive(true);
        _invulnerableEffect.GetComponent<InvulnerabilityFX>().Enable(seconds);
        StartCoroutine(SetInvulnerable(seconds));
    }

    public void Shield(int level)
    {
        GetComponent<Shield>().Enable(level);
    }

    public void FireRate(int level)
    {
        Weapon weapon = GetComponent<Weapon>();
        weapon.fireRate /= level + 1;
    }

    public void Energy(int level)
    { }

    public void Speed(int level)
    { }

    public void Drone(int level)
    { }

    private IEnumerator SetInvulnerable(float seconds)
    {
        yield return new WaitForSeconds(seconds + 0.25f);
        while (GameManager.Instance.isPaused)
        {
            yield return new WaitForFixedUpdate();
        }
        this.GetComponent<Health>().Invulnerable(false);
        _invulnerableEffect.SetActive(false);
    }
}