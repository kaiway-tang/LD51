using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class specialAtkMan : MonoBehaviour
{
    [SerializeField] GameObject[] enemies;
    [SerializeField] Transform trfm;
    Vector2 bombPos; bool skipFirst;

    private void Start()
    {
        StartCoroutine(doAtk());
    }

    IEnumerator doAtk()
    {
        while (true)
        {
            if (!skipFirst)
            {
                skipFirst = true;
            }
            else
            {
                trfm.position = PlayerController.plyrTrfm.position;
                trfm.Rotate(Vector3.forward * Random.Range(0,360));
                trfm.position += trfm.up * 30;
                Instantiate(enemies[Random.Range(0, enemies.Length)]);
            }
            yield return new WaitForSeconds(manager.difficultyScaler(35, 8, 250)+Random.Range(-2,3));
        }
    }
}
