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
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        Beam shot = otherCollider.gameObject.GetComponent<Beam>();
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