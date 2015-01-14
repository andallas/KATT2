using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Shield : MonoBehaviour
{
    public int shieldLevel = 0;
    public List<GameObject> effects;

    private GameObject level1, level2, level3;

    void Awake()
    {
        level1 = (GameObject)Instantiate(effects[0], this.gameObject.transform.position, effects[0].transform.rotation);
        level1.transform.SetParent(this.gameObject.transform);
        level1.SetActive(false);

        level2 = (GameObject)Instantiate(effects[1], this.gameObject.transform.position, effects[1].transform.rotation);
        level2.transform.SetParent(this.gameObject.transform);
        level2.SetActive(false);

        level3 = (GameObject)Instantiate(effects[2], this.gameObject.transform.position, effects[2].transform.rotation);
        level3.transform.SetParent(this.gameObject.transform);
        level3.SetActive(false);
    }

    void OnEnable()
    {
        Enable(shieldLevel);
    }

    public void Damage(ref int damage)
    {
        int temp = shieldLevel;
        shieldLevel -= damage;
        damage -= temp;
        damage = damage <= 0 ? 0 : damage;
        shieldLevel = shieldLevel <= 0 ? 0 : shieldLevel;
        Enable(shieldLevel);
    }

    public void Enable(int level)
    {
        shieldLevel = level;

        if(level1 == null || level2 == null || level3 == null)
        {
            return;
        }
        level1.SetActive(false);
        level2.SetActive(false);
        level3.SetActive(false);

        switch(shieldLevel)
        {
            case 1:
                level1.SetActive(true);
                break;
            case 2:
                level2.SetActive(true);
                break;
            case 3:
                level3.SetActive(true);
                break;
            default: break;
        }
    }
}