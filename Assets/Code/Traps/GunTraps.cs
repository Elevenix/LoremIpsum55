using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTraps : MonoBehaviour
{
    [SerializeField] private float cadenceShoot = 1;
    [SerializeField] private float bulletSpeed = 1;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject bulletKill;

    private void Start()
    {
        StartCoroutine(LoopShoot());
    }

    private IEnumerator LoopShoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(cadenceShoot);
            BulletTrap bt = Instantiate(bulletKill, spawnPoint.position, Quaternion.identity).GetComponent<BulletTrap>();
            bt.direction = transform.right;
            bt.speed = bulletSpeed;
        }
    }
}
