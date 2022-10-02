using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class feet : MonoBehaviour
{
    [SerializeField] PlayerController plyrScript;

    private void OnTriggerEnter2D(Collider2D col)
    {
        plyrScript.onGround = true;
        plyrScript.remainingJumps = 2;
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        plyrScript.onGround = false;
    }
}
