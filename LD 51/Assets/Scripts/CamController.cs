using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    [SerializeField] Transform plyrTrfm, camTarget;
    [SerializeField] float focusRadius = 2;
    public static Camera mainCam;
    Transform trfm;

    Vector2 focusPoint;
    Vector3 difference;

    void Awake()
    {
        focusPoint = plyrTrfm.position;
        trfm = transform;
        trfm.parent = null;
        mainCam = GetComponent<Camera>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        cameraGlide();
        //cameraLerp();
    }

    void cameraGlide()
    {
        difference.x = (camTarget.position.x - trfm.position.x)*.1f;
        difference.y = (camTarget.position.y - trfm.position.y)*.1f;

        trfm.position += difference;
    }

    void cameraLerp()
    {
        Vector2 targetPoint = plyrTrfm.position;
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
        trfm.localPosition = new Vector3(focusPoint.x, trfm.position.y, trfm.position.z);
    }
}
