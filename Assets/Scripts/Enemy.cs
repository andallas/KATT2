﻿using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Weapon[] weapons;
    private bool hasSpawn;
    private Move movement;

    void Awake()
    {
        weapons = GetComponents<Weapon>();
        movement = GetComponent<Move>();
    }

    void Start()
    {
        Enable(false);
    }

    void Update()
    {
        if (!hasSpawn)
        {
            if (renderer.IsVisibleFrom(Camera.main))
            {
                Enable(true);
            }
        }
        else
        {
            if(GameManager.Instance.levelActive && !GameManager.Instance.isPaused)
            {
                foreach (Weapon weapon in weapons)
                {
                    if (weapon != null && weapon.CanAttack)
                    {
                        weapon.Attack(true);
                        AudioManager.Instance.PlaySFX("Enemy Laser");
                    }
                }
            }

            if (!renderer.IsVisibleFrom(Camera.main))
            {
                Destroy(gameObject);
            }
        }
    }

    private void Enable(bool enable)
    {
        hasSpawn = enable;
        collider2D.enabled = enable;
        movement.enabled = enable;

        foreach (Weapon weapon in weapons)
        {
            weapon.enabled = enable;
        }
    }
}