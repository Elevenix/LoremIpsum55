using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallClone : MonoBehaviour
{
    [SerializeField] private GameObject clonePlayer;

    private GameObject playerLaunch;
    private bool first = true;

    private RandomSounds RandomSounds;

    // Start is called before the first frame update
    void Start()
    {
        RandomSounds = GetComponent<RandomSounds>();
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
            Instantiate(clonePlayer, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        } 
    }
}
