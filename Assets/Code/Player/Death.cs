using System.Collections;
using UnityEngine;

public class Death : MonoBehaviour
{
    [SerializeField] private float delayRestart = 1;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject bloodParticles;
    [SerializeField] private Shake cameraShake;
    [SerializeField] private MonoBehaviour[] toDisableAtDeath;

    private Rigidbody2D rb;
    private BoxCollider2D col;
    private bool isDead = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isDead && collision.CompareTag("Death"))
        {
            isDead = true;
            StartCoroutine(DeathCoroutine());
        }
    }

    private IEnumerator DeathCoroutine()
    {
        col.enabled = false;
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
        anim.SetTrigger("Death");
        CameraManager.Instance.Shaker(cameraShake);
        Instantiate(bloodParticles, transform.position, Quaternion.identity);
        foreach (MonoBehaviour mb in toDisableAtDeath)
        {
            mb.enabled = false;
        }

        yield return new WaitForSeconds(delayRestart);

        GameManager.Instance.GetSceneManager().RestarttLevel();
    }
}
