using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    [SerializeField] private Transform cam;

    private Vector3 initPos;
    private Coroutine camShaking;

    private void Awake()
    {
        // Singleton
        if(Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        initPos = cam.position;
    }

    /// <summary>
    /// Camera Shake
    /// </summary>
    /// <param name="delay"> Delay of the shake </param>
    /// <param name="magnitude"> The force of the shake (distance of the initial position) </param>
    /// <param name="amount"> The number of shock, cannot be zero </param>
    public void Shaker(float delay, float magnitude, int amount)
    {
        if(camShaking != null)
        {
            StopCoroutine(camShaking);
        }
        camShaking = StartCoroutine(Shaking(delay, magnitude, amount));
    }

    /// <summary>
    /// Camera Shake
    /// </summary>
    /// <param name="shake"> The shake parameters</param>
    public void Shaker(Shake shake)
    {
        if (camShaking != null)
        {
            StopCoroutine(camShaking);
        }
        camShaking = StartCoroutine(Shaking(shake.delay, shake.magnitude, shake.amount));
    }

    private IEnumerator Shaking(float delay, float magnitude, int amount)
    {
        if (amount <= 0)
            yield return null;
        float deltaDelay = delay / amount;
        for(int i = 0; i < amount; i++)
        {
            float randX = Random.Range(cam.position.x - magnitude, cam.position.x + magnitude);
            float randY = Random.Range(cam.position.y - magnitude, cam.position.y + magnitude);
            cam.position = new Vector3(randX, randY, cam.position.z);
            yield return new WaitForSeconds(deltaDelay);
        }
        cam.position = initPos;
    }    
}
