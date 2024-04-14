using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPlatform : MonoBehaviour
{
    [SerializeField] private PlatformMovable platform;
    [SerializeField] private Animator anim;
    [SerializeField] private Shake shakeAction;

    private bool usedOnce = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !usedOnce)
        {
            usedOnce = true;
            if (shakeAction != null)
                CameraManager.Instance.Shaker(shakeAction);
            anim.SetTrigger("Press");
            platform.Launch(true);
        }
    }
}
