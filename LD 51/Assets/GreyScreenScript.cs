using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class GreyScreenScript : MonoBehaviour
{

    PostProcessVolume ppv;
    // Start is called before the first frame update
    void Start()
    {
        ppv = GetComponent<PostProcessVolume>();
        ppv.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerController.self.hp <= 0)
        {
            ppv.enabled = true;
            Time.timeScale = 0.2f;
        }
    }
}
