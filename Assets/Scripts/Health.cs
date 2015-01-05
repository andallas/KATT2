using UnityEngine;

public class Health : MonoBehaviour
{
    public int healthPoints;
    public bool isEnemy;
    private bool isInvunlerable = false;

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        Projectile projectile = otherCollider.gameObject.GetComponent<Projectile>();
        if (projectile != null)
        {
            if (projectile.isEnemyShot != isEnemy)
            {
                Damage(projectile.damage);
                ProjectilePool.Instance.Recycle(projectile.gameObject);
            }
        }
    }

    public void Invulnerable(bool enable)
    {
        isInvunlerable = enable;
    }

    public void Damage(int damageCount)
    {
        if(!isInvunlerable)
        {
            healthPoints -= damageCount;
        }

        if(healthPoints <= 0)
        {
            SpecialFXHelper.Instance.Explosion(transform.position);
            AudioManager.Instance.PlaySFX("Explosion");
            if(isEnemy)
            {
                Destroy(gameObject);
                GameManager.Instance.AddScore(gameObject.GetComponent<Enemy>().scoreValue);
            }
            else
            {
                GameManager.Instance.LoseLife();
            }
        }
    }
}