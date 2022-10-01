using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPEntity : MonoBehaviour
{
    public int hp;
    
    public bool takeDamage(int amount) //returns true if entity is killed
    {
        hp -= amount;
        if (hp <= 0)
        {
            return true;
            Destroy(gameObject);
        }
        return false;
    }
}
