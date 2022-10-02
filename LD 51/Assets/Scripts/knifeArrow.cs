using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knifeArrow : MonoBehaviour
{
    [SerializeField] Transform trfm, targetKnife;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        trfm.position = PlayerController.plyrTrfm.position;
        trfm.rotation = Quaternion.AngleAxis(Mathf.Atan2(trfm.position.y - targetKnife.position.y, trfm.position.x - targetKnife.position.x) * Mathf.Rad2Deg + 90, Vector3.forward);
    }
}
