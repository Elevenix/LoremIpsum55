using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform gunTransform;
    [SerializeField] private Transform spawnPos;
    [SerializeField] private float speedEjectionClone = 1f;
    [SerializeField] private GameObject clone;

    private Vector3 mousePos;
    private Vector2 lookdir;

    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ShootClone();
        }
    }

    void FixedUpdate()
    {
        GunRotation();
    }

    /// <summary>
    /// Set the rotation of the gun by the position of the mouse
    /// </summary>
    private void GunRotation()
    {
        lookdir = mousePos - transform.position;
        float angle = Mathf.Atan2(lookdir.y, lookdir.x) * Mathf.Rad2Deg - 90f;
        gunTransform.rotation = Quaternion.Euler(0, 0, angle);
    }

    /// <summary>
    /// Shoot a clone 
    /// </summary>
    private void ShootClone()
    {
        if (clone != null)
        {
            GameObject instanceClone = Instantiate(clone, spawnPos.position, Quaternion.identity);
            // TODO : shoot the ball
            if (instanceClone.TryGetComponent(out Rigidbody2D rbClone))
            {
                rbClone.velocity = lookdir.normalized * speedEjectionClone;
            }
        }
    }

    private void OnDisable()
    {
        gunTransform.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        gunTransform.gameObject.SetActive(true);
    }
}
