using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallClone : MonoBehaviour
{
    [SerializeField] private GameObject clonePlayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Default"))
        {
            Instantiate(clonePlayer, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
