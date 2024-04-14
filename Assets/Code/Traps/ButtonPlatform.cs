using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPlatform : MonoBehaviour
{
    [SerializeField] private PlatformMovable platform;
    [SerializeField] private Animator anim;

    private bool usedOnce = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !usedOnce)
        {
            usedOnce = true;
            anim.SetTrigger("Press");
            platform.Launch(true);
        }
    }
}
