using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public int damage;
    public bool isEnemyShot;

    void Awake()
    {
        SetSprite();
    }

    private virtual void SetSprite()
    {}
}