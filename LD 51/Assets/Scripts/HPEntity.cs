using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPEntity : MonoBehaviour
{
    public int ID, maxHP, hp;
    public static int playerID = 1, enemy = 0;
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
    public bool takeDamage(int amount, int ignoreID = -1, Transform source = null) //returns false if entity is ignoreID
    {
        if (ignoreID == ID) return false;
        hp -= amount;
        if (hp <= 0)
        {
            for (int i = 0; i < 3; i++)
            {
               Transform dfxTrfm = Instantiate(deathFX, transform.position, Quaternion.identity).transform;
               dfxTrfm.rotation = Quaternion.AngleAxis(Mathf.Atan2(source.position.y - dfxTrfm.position.y, source.position.x - dfxTrfm.position.x) * Mathf.Rad2Deg + 90, Vector3.forward);
            }
            if (ID == playerID)
            {
                CamController.setTrauma(30);
                gameObject.SetActive(false);
            } else
            {
                manager.self.incrementScore(1);
                CamController.setTrauma(20);
                Destroy(gameObject);
            }
        }
        return true;
    }
}
