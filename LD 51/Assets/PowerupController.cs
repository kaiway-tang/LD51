using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PowerupController : MonoBehaviour
{

    [SerializeField] Transform[] spawnPoints;
    [SerializeField] Powerup powerup;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawn());
    }

    IEnumerator spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(10);
            Instantiate(powerup, Vector3.zero, Quaternion.identity).GetComponent<Powerup>().setPoints(spawnPoints.Select(point => (Vector2)point.position).ToArray());
            yield return new WaitForSeconds(20);
        }
    }
}
