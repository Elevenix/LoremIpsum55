using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    [SerializeField] private Dialog dialogEn;
    [SerializeField] private Dialog dialogFr;
    private bool alreadyTouched = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!alreadyTouched && collision.CompareTag("Player"))
        {
            alreadyTouched = true;
            if(PlayerPrefs.GetInt("Language") == 0)
                GameManager.Instance.LaunchDialog(dialogEn);
            else
                GameManager.Instance.LaunchDialog(dialogFr);
        }
    }
}
