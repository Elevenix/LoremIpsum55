using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovable : MonoBehaviour
{
    [Header("The platform to move")]
    [SerializeField] private Transform platform;

    [Space(6)]
    [Header("Position start/end")]
    [SerializeField] private Transform startPos;
    [SerializeField] private Transform endPos;

    [Space(6)]
    [Header("Movement Parameters")]
    [SerializeField] private float speed = 5;
    [SerializeField] private float delayMove;
    [SerializeField] private bool onlyOne;
    [SerializeField] private bool stop = true;
    [SerializeField] private bool platformAtStartPos = true;

    private int direction = 1;

    private void Start()
    {
        if(!platformAtStartPos)
            platform.position = startPos.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!stop)
        {
            Vector2 target = CurrentMovementTarget();

            float distance = (target - (Vector2)platform.position).magnitude;

            platform.position = Vector2.Lerp(platform.position, target, speed * Time.deltaTime / distance);

            if (distance <= .1f)
            {
                direction *= -1;
                StartCoroutine(DelayMovement());
            }
        }
    }

    /// <summary>
    /// Launch the platform only once
    /// </summary>
    /// <param name="oneTime"> True, launch once</param>
    public void Launch(bool oneTime = false)
    {
        this.onlyOne = oneTime;
        stop = false;
    }

    /// <summary>
    /// Return the targeted position
    /// </summary>
    /// <returns></returns>
    private Vector2 CurrentMovementTarget()
    {
        if(direction == 1)
        {
            return endPos.position;
        }
        else
        {
            if (onlyOne)
                stop = true;
            return startPos.position;
        }
    }

    private IEnumerator DelayMovement()
    {
        stop = true;
        yield return new WaitForSeconds(delayMove);
        if(!onlyOne)
            stop = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(startPos.position, endPos.position);
    }
}
