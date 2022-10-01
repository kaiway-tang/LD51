using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float focusRadius = 2;

    Vector2 focusPoint;

    void Awake()
    {
        focusPoint = player.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 targetPoint = player.position;
        if (focusRadius > 0f)
        {
            float distance = Mathf.Abs(targetPoint.x - focusPoint.x);
            if (distance > focusRadius)
            {
                focusPoint = Vector2.Lerp(
                    targetPoint, focusPoint, focusRadius / distance
                );
            }
        }
        else
        {
            focusPoint = targetPoint;
        }
        transform.localPosition = new Vector3(focusPoint.x, transform.position.y, transform.position.z);
    }
}
