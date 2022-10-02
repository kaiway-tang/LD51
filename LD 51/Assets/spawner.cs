using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject enemy;
    [SerializeField] float delay = 2;
    [SerializeField] Vector2[] spawnpoints;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawn());
    }

    IEnumerator spawn()
    {
        while (true)
        {
            Instantiate(enemy, Vector3.zero, Quaternion.identity);
            yield return new WaitForSeconds(manager.difficultyScaler(4,1,160));
        }
    }
}
