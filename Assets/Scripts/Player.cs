using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;

    private Vector2 movement;

    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        movement = new Vector2(inputX, inputY) * speed;
        movement = Vector2.ClampMagnitude(movement, speed);

        bool shoot = Input.GetButton("Fire1");
        shoot |= Input.GetButton("Fire2");

        if(shoot)
        {
            Weapon weapon = GetComponent<Weapon>();
            if(weapon != null)
            {
                weapon.Attack(false);
            }
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
}