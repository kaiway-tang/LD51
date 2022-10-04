using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class txtObj : MonoBehaviour
{
    [SerializeField] Transform trfm;
    [SerializeField] Vector3 rise;
    [SerializeField] SpriteRenderer rend;
    [SerializeField] Color alpha;

    // Update is called once per frame
    private void Start()
    {
        Destroy(gameObject, 1f);
    }
    void FixedUpdate()
    {
        trfm.position += rise;
        rend.color -= alpha;
    }
}
