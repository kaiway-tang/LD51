using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public int spd;
    private void FixedUpdate()
    {
        transform.Rotate(Vector3.forward * spd);
    }
}
