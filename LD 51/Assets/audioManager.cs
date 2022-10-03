using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    [SerializeField] AudioClip[] audioClips;
    public static audioManager self;
    public static int music;
    // Start is called before the first frame update
    void Start()
    {
        self = GetComponent<audioManager>();
        DontDestroyOnLoad(gameObject);
    }

    public static void play(int clip)
    {
        
    }
}
