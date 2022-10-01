using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPEntity : MonoBehaviour
{
    public int maxHP, hp;
    
    public bool heal(int amount) //returns true if entity is full hp
    {
        hp += amount;
        if (hp > maxHP)
        {
            hp = maxHP;
            return true;
        }
        return false;
    }
    public bool takeDamage(int amount) //returns true if entity is killed
    {
        hp -= amount;
        if (hp <= 0)
        {
            Destroy(gameObject);
            return true;
        }
        return false;
    }
}
