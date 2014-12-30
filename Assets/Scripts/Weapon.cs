using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject shotPrefab;
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

            GameObject shotObject = (GameObject)Instantiate(shotPrefab);
            shotObject.transform.position = transform.position;

            Beam shot = shotObject.GetComponent<Beam>();
            if(shot != null)
            {
                shot.isEnemyShot = isEnemy;
            }

            Move move = shotObject.GetComponent<Move>();
            if(move != null)
            {
                move.direction = isEnemy ? -this.transform.up : this.transform.up;
            }
        }
    }
}