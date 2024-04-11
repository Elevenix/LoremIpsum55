using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private float speedLetters = .1f;

    [Space(8)]
    [SerializeField] [TextArea] private string[] testDialog;

    private bool textFinished = false;
    private int actualText = 0;
    private Coroutine delayLetters;
    private string[] dialog;

    // Start is called before the first frame update
    void Start()
    {
        LaunchDialog(testDialog);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && dialog != null && actualText >= 0)
        {
            if (textFinished)
            {
                NextText();
            }
            else
            {
                PassText();
            }
        }
    }

    public void LaunchDialog(string[] d)
    {
        StopDialog();
        dialog = d;
        actualText = 0;
        delayLetters = StartCoroutine(DelayLetters(dialog[actualText]));
    }

    private void StopDialog()
    {
        dialog = null;
        if (delayLetters != null)
            StopCoroutine(delayLetters);
        actualText = 0;
        textFinished = true;
        textMesh.text = "";
    }

    private void NextText()
    {
        actualText++;
        if(actualText < dialog.Length)
            delayLetters = StartCoroutine(DelayLetters(dialog[actualText]));
        else
            StopDialog();
    }

    private void PassText()
    {
        if (delayLetters != null)
            StopCoroutine(delayLetters);
        textMesh.text = dialog[actualText];
        textFinished = true;
    }

    /// <summary>
    /// Delay apparation letters
    /// </summary>
    /// <param name="text"> the string to delayed </param>
    /// <returns></returns>
    private IEnumerator DelayLetters(string text)
    {
        textFinished = false;
        string newText = "";
        for(int i=0; i < text.Length; i++)
        {
            newText += text[i];
            if(text[i] != ' ')
            {
                textMesh.text = newText;
                yield return new WaitForSeconds(speedLetters);
            }
        }
        textFinished = true;
    }

}
