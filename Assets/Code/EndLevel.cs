using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour
{
    private bool touchOnce = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !touchOnce)
        {
            touchOnce = true;
            GameManager.Instance.GetSceneManager().NextLevel();
        }
    }
}
