using UnityEngine;

public class Health : MonoBehaviour
{
    public int healthPoints;
    public bool isEnemy;

    public void Damage(int damageCount)
    {
        healthPoints -= damageCount;

        if(healthPoints <= 0)
        {
            SpecialFXHelper.Instance.Explosion(transform.position);
            AudioManager.Instance.PlaySFX("Explosion");
            if(isEnemy)
            {
                Destroy(gameObject);
            }
            else
            {
                GameManager.Instance.LoseLife();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        Projectile shot = otherCollider.gameObject.GetComponent<Projectile>();
        if(shot != null)
        {
            if(shot.isEnemyShot != isEnemy)
            {
                Damage(shot.damage);
                Destroy(shot.gameObject);
            }
        }
    }
}