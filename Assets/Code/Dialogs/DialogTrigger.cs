using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    [SerializeField] private Dialog dialog;
    private bool alreadyTouched = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!alreadyTouched && collision.CompareTag("Player"))
        {
            alreadyTouched = true;
            GameManager.Instance.LaunchDialog(dialog);
        }
    }
}
