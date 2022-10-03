using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{

    Vector2[] spawnLocations;
    Vector2 spawnLocation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void setPoints(Vector2[] points)
    {
        spawnLocations = points;
        spawnLocation = spawnLocations[Random.Range(0, spawnLocations.Length)];
        transform.position = spawnLocation;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 7 && collision.GetComponent<HPEntity>().ID == 1)
        {
            // this is the player
            manager.self.applyPowerup();
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
