using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float fireRate = 0.25f;

    private float shootCooldown;

    void Start()
    {
        shootCooldown = 0f;
    }

    void Update()
    {
        if (!GameManager.Instance.isPaused)
        {
            if (shootCooldown > 0)
            {
                shootCooldown -= Time.deltaTime;
            }
        }
    }

    public bool CanAttack { get { return shootCooldown <= 0f; } }

    public void Attack(bool isEnemy)
    {
        if(CanAttack)
        {
            shootCooldown = fireRate;

            GameObject projectileObject = ProjectilePool.Instance.Create(transform.position);

            if(projectileObject == null)
            {
                return;
            }

            Projectile projectile = projectileObject.GetComponent<Projectile>();
            if (projectile != null)
            {
                projectile.isEnemyShot = isEnemy;
                projectile.SetSprite();
            }

            Move move = projectileObject.GetComponent<Move>();
            if(move != null)
            {
                move.direction = isEnemy ? -this.transform.up : this.transform.up;
            }

            projectileObject.SetActive(true);

            if(isEnemy)
            {
                AudioManager.Instance.PlaySFX("Enemy Laser");
            }
            else
            {
                AudioManager.Instance.PlaySFX("Player Laser");
            }
            
        }
    }
}