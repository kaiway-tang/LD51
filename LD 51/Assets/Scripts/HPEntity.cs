using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPEntity : MonoBehaviour
{
    public int ID, maxHP, hp;
    public static int playerID = 1;
    [SerializeField] GameObject deathFX;
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
    public bool takeDamage(int amount, int ignoreID = -1) //returns false if entity is ignoreID
    {
        if (ignoreID == ID) return false;
        hp -= amount;
        if (hp <= 0)
        {
            Instantiate(deathFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        return true;
    }
}
