using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform gunTransform;
    [SerializeField] private Transform spawnPos;
    [SerializeField] private float speedEjectionClone = 1f;
    [SerializeField] private GameObject ballClone;

    private Vector3 mousePos;
    private Vector2 lookdir;

    private RandomSounds randomSounds;

    // Start is called before the first frame update
    void Start()
    {
        randomSounds = GetComponent<RandomSounds>();
    }


    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ShootClone();
            randomSounds.PlaySound("Shoot");
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
        if (ballClone != null)
        {
            GameObject instanceClone = Instantiate(ballClone, spawnPos.position, Quaternion.identity);
            if (instanceClone.TryGetComponent(out Rigidbody2D rbClone))
            {
                rbClone.velocity = lookdir.normalized * speedEjectionClone;
            }
            if(instanceClone.TryGetComponent(out BallClone ball))
            {
                ball.SetPlayerLaunch(this.gameObject);
            }
        }
    }

    private void OnDisable()
    {
        gunTransform.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        GunRotation();
        gunTransform.gameObject.SetActive(true);
    }
}
