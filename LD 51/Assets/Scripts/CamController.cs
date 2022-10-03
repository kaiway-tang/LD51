using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public Transform plyrTrfm, camTarget;
    [SerializeField] float focusRadius = 2;
    public static Camera mainCam;
    Transform trfm;
    float glideRate;
    Vector3 leftRotatedZero = new Vector3(0, 0, 360);

    public static CamController self;

    Vector2 focusPoint;
    Vector3 difference;

    void Awake()
    {
        focusPoint = plyrTrfm.position;
        trfm = transform;
        trfm.parent = null;
        mainCam = GetComponent<Camera>();
        self = GetComponent<CamController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        glideRate = .1f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        cameraGlide();
        processTrauma();
        //cameraLerp();
    }

    void cameraGlide()
    {
        difference.x = (camTarget.position.x - trfm.position.x)*glideRate;
        difference.y = (camTarget.position.y - trfm.position.y)*glideRate;

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

    int hardLock;

    public void hardLockOn()
    {
        if (hardLock == 0) glideRate = .8f;
        hardLock++;
    }

    public void hardLockOff()
    {
        hardLock--;
        if (hardLock == 0) glideRate = .1f;
    }


    [SerializeField] float strength;
    public static int trauma;
    static float instance;
    static Vector3 shift;
    void processTrauma()
    {
        if (trauma > 0)
        {
            trauma--;
            instance = trauma * trauma * strength;
            shift.x = Random.Range(-instance, instance);
            shift.y = Random.Range(-instance, instance)/2;
            trfm.position += shift;
            trfm.Rotate(Vector3.forward* Random.Range(-instance,instance));
        }

        if (Mathf.Abs(trfm.localEulerAngles.z) < .04f)
        {
            trfm.localEulerAngles = Vector3.zero;
        }
        else if (trfm.localEulerAngles.z < 180)
        {
            trfm.localEulerAngles += (Vector3.zero - trfm.localEulerAngles) * .1f;
        }
        else
        {
            trfm.localEulerAngles += (leftRotatedZero - trfm.localEulerAngles) * .1f;
        }
    }

    public static void setTrauma(int amount)
    {
        if (trauma < amount) trauma = amount;
    }
}
