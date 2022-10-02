using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombMan : MonoBehaviour
{
    [SerializeField] GameObject bomb;
    Vector2 bombPos; bool skipFirst;

    private void Start()
    {
        StartCoroutine(startChain());
    }

    IEnumerator startChain()
    {
        while (true)
        {
            if (!skipFirst)
            {
                skipFirst = true;
            }
            else
            {
                bombPos.x = PlayerController.plyrTrfm.position.x - 15;
                bombPos.y = PlayerController.plyrTrfm.position.y;
                StartCoroutine(placeChain());
            }
            yield return new WaitForSeconds(manager.difficultyScaler(35, 10, 250)+Random.Range(-2,3));
        }
    }

    IEnumerator placeChain()
    {
        for (int i = 0; i < 7; i++)
        {
            Instantiate(bomb, bombPos, Quaternion.identity);
            bombPos.x += 5;
            yield return new WaitForSeconds(.1f);
        }
    }
}
