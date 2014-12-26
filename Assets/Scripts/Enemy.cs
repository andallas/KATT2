using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Weapon[] weapons;

    void Awake()
    {
        weapons = GetComponents<Weapon>();
    }

    void Update()
    {
        foreach(Weapon weapon in weapons)
        {
            if (weapon != null && weapon.CanAttack)
            {
                weapon.Attack(true);
            }
        }
    }
}