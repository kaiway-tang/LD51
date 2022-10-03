using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class spawner : MonoBehaviour
{
    public SpaceshipController enemy;
    public laserShip shieldEnemy;
    [SerializeField] Transform[] spawnpoints;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawn());
    }

    


    IEnumerator spawn()
    {
        while (true)
        {
            if(Random.Range(0, 100) < 10)
            {
                Vector2 spawnPoint = spawnpoints[Random.Range(0, spawnpoints.Length)].position;
                Instantiate(shieldEnemy, Vector3.zero, Quaternion.identity).GetComponent<SpaceshipController>().setPoints(spawnpoints.Select(point => (Vector2)point.position).ToArray());

            }
            Instantiate(enemy, Vector3.zero, Quaternion.identity).GetComponent<SpaceshipController>().setPoints(spawnpoints.Select(point => (Vector2)point.position).ToArray());
            yield return new WaitForSeconds(manager.difficultyScaler(4,1,160));
        }
    }
}
