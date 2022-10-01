using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipController : MonoBehaviour
{

    [SerializeField] Transform[] dropOffPoints;
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
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        int randomIndex = Random.Range(0, dropOffPoints.Length);
        Debug.Log(randomIndex);
        dropOffPoint = dropOffPoints[randomIndex].position;
        float angle = Random.value * Mathf.PI * 2f;
        startPoint = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
        transform.position = startPoint;
        direction = dropOffPoint - startPoint;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(dropOffPoint, transform.position);
        if(distance < 2)
        {
            body.velocity = direction.normalized * Mathf.Lerp(0, speed, distance / direction.magnitude);

        }
        else
        {
            body.velocity = direction.normalized * speed;
        }
        if (departing)
        {
            body.velocity = direction.normalized * speed;
            return;
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
                for(int i = 0; i < enemyCount; i++)
                {
                    Instantiate(enemy, transform.position, Quaternion.identity);

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
