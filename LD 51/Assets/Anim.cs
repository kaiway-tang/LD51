using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Anim : MonoBehaviour
{
    [SerializeField] Image leftKnife;
    [SerializeField] Image rightKnife;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowKnives(float y)
    {
        RectTransform trfml = leftKnife.gameObject.GetComponent<RectTransform>();
        RectTransform trfmr = rightKnife.gameObject.GetComponent<RectTransform>();
        trfml.localPosition = new Vector3(trfml.localPosition.x, y, trfml.localPosition.z);
        trfmr.localPosition = new Vector3(trfmr.localPosition.x, y, trfmr.localPosition.z);
        leftKnife.color = Color.white;
        rightKnife.color = Color.white;
    }

    public void HideKnives()
    {
        leftKnife.color = new Vector4(255, 255, 255, 0);
        rightKnife.color = new Vector4(255, 255, 255, 0);
    }

}
