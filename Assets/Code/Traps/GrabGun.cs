using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabGun : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Give the gun to the player
            GameManager.Instance.GiveGun();
            Destroy(this.gameObject);
        }
    }
}
