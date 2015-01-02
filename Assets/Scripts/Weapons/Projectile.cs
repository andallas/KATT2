using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public int damage;
    public bool isEnemyShot;

    protected enum BeamColor { red, green, blue, magenta, yellow, cyan, rose, orange, chartreuseGreen, springGreen, azure, violet }

    void Awake()
    {
        SetSprite();
    }

    public virtual void SetSprite()
    {}
}