using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    [SerializeField] private Camera cam;

    private Vector3 initPos;
    private Coroutine camShaking;
    private float initSizeProjection;
    private bool isZooming = false;

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
        initPos = cam.transform.position;
        initSizeProjection = cam.orthographicSize;
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
            float randX = Random.Range(cam.transform.position.x - magnitude, cam.transform.position.x + magnitude);
            float randY = Random.Range(cam.transform.position.y - magnitude, cam.transform.position.y + magnitude);
            cam.transform.position = new Vector3(randX, randY, cam.transform.position.z);
            yield return new WaitForSeconds(deltaDelay);
        }
        if(!isZooming)
            cam.transform.position = initPos;
    }

    /// <summary>
    /// Camera Zoom 
    /// </summary>
    /// <param name="posZoomed"> Position to zoom (z value not set)</param>
    /// <param name="zoomValue"> Multiply of the zoom (cannot be inferior or equal to zero) </param>
    public void Zoom(Vector3 posZoomed, float zoomValue)
    {
        isZooming = true;
        cam.transform.position = new Vector3(posZoomed.x,posZoomed.y, cam.transform.position.z);
        cam.orthographicSize *= zoomValue;
    }

    /// <summary>
    /// Return the initial value of the camera zoom
    /// </summary>
    public void DeZoom()
    {
        isZooming = false;
        cam.orthographicSize = initSizeProjection;
        cam.transform.position = initPos;
    }
}
