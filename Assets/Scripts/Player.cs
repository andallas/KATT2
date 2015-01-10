using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public float speed;
    public GameObject invulnerableEffect;

    private Vector2 movement;
    private Transform _transform;
    private float halfWidth;
    private float halfHeight;
    private GameObject _invulnerableEffect;
    private Animator animator;

    void Awake()
    {
        _invulnerableEffect = (GameObject)Instantiate(invulnerableEffect, this.transform.position, Quaternion.identity);
        _invulnerableEffect.transform.SetParent(this.transform);
        _invulnerableEffect.SetActive(false);
        _transform = transform;
        halfWidth = renderer.bounds.size.x / 2;
        halfHeight = renderer.bounds.size.y / 2;
        animator = GetComponentInChildren<Animator>();
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
        rigidbody2D.velocity = movement;
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
        this.GetComponent<Renderer>().enabled = false;
        this.GetComponent<Collider2D>().enabled = false;
    }

    public void Invulnerable(float seconds)
    {
        this.GetComponent<Health>().Invulnerable(true);
        _invulnerableEffect.SetActive(true);
        StartCoroutine(SetInvulnerable(seconds));
    }

    private IEnumerator SetInvulnerable(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        this.GetComponent<Health>().Invulnerable(false);
        _invulnerableEffect.SetActive(false);
    }
}