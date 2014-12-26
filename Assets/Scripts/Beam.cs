using UnityEngine;

public class Beam : MonoBehaviour
{
    public int damage;
    public bool isEnemyShot;

    void Start()
    {
        Destroy(gameObject, 20);
    }
}