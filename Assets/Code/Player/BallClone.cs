using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallClone : MonoBehaviour
{
    [SerializeField] private GameObject clonePlayer;
    [SerializeField] private GameObject particleSpawn;

    private GameObject playerLaunch;
    private bool first = true;

    private void Start()
    {
        // Destroy gameObject 10 seconds later
        Destroy(this.gameObject, 10);
    }

    public void SetPlayerLaunch(GameObject go)
    {
        playerLaunch = go;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (first && !collision.CompareTag("GroundThrough") && collision.gameObject != playerLaunch)
        {
            first = false;
            if(particleSpawn != null)
                Instantiate(particleSpawn, transform.position, Quaternion.identity);
            Instantiate(clonePlayer, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        } 
    }
}
