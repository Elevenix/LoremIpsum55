using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    private static MusicController musicController;

    // Start is called before the first frame update
    private void Awake()
    {
        if(musicController != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            musicController = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
