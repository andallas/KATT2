using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public float speed;

    private Vector2 movement;
    private Transform _transform;
    private float halfWidth;
    private float halfHeight;

    void Start()
    {
        _transform = transform;
        halfWidth = renderer.bounds.size.x / 2;
        halfHeight = renderer.bounds.size.y / 2;
    }

    void Update()
    {
        if (GameManager.Instance.levelActive && !GameManager.Instance.isPaused)
        {
            float inputX = Input.GetAxis("Horizontal");
            float inputY = Input.GetAxis("Vertical");

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
                    AudioManager.Instance.PlaySFX("Player Laser");
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
        gameObject.GetComponent<Renderer>().enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = false;
    }
}