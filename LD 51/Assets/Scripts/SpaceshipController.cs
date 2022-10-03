using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipController : mobileEntity
{

    Vector2[] dropOffPoints;
    [SerializeField] float radius = 20;
    [SerializeField] float speed = 5;
    [SerializeField] GameObject dropOffProgress;
    [SerializeField] GameObject enemy;
    [SerializeField] int enemyCount = 1;
    Rigidbody2D body;
    Vector2 direction;
    Vector2 dropOffPoint;
    Vector2 startPoint;

    [SerializeField] float dropOffTime = 2;

    bool droppingOff = false;
    bool departing = false;
    float dropOffRadius = 0.5f;
    float currentDropOffProgress = 0;

    public void setPoints(Vector2[] points)
    {
        dropOffPoints = points;

    }
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        dropOffPoint = dropOffPoints[Random.Range(0, dropOffPoints.Length)];
        float angle = Random.value * Mathf.PI * 2f;
        startPoint = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
        transform.position = startPoint;
        direction = dropOffPoint - startPoint;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(dropOffPoint, transform.position);
        if (departing)
        {
            body.velocity = direction.normalized * speed;
            return;
        }
        if (distance < 1)
        {
            body.velocity = direction.normalized * speed * 0.05f;

        }
        else
        {
            body.velocity = direction.normalized * speed;
        }

        if(droppingOff)
        {
            if(currentDropOffProgress < dropOffTime)
            {
                currentDropOffProgress += Time.deltaTime;
                dropOffProgress.transform.localScale = new Vector3(currentDropOffProgress, dropOffProgress.transform.localScale.y, dropOffProgress.transform.localScale.z);
            }
            else
            {
                for(int i = 0; i < Random.Range(1,(manager.difficulty/40)+1); i++)
                {
                    Instantiate(enemy, transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f,1f),0), Quaternion.identity);

                }
                droppingOff = false;
                departing = true;
            }
            return;
        }
        if(Vector2.Distance(dropOffPoint, transform.position) < dropOffRadius)
        {
            droppingOff = true;
        }
    }
}
