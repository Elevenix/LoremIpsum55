using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum TextColor
{
    red,
    yellow,
    blue,
    white,
    black,
    pink,
    orange,
    purple,
    green,
}

public class DialogManager : MonoBehaviour
{
    [SerializeField] private float speedLetters = .1f;

    [SerializeField] private TextColor[] colorPlayers;

    private bool textFinished = true;
    private Coroutine letterCoroutine;
    private TextMeshPro previousTextMesh;

    public bool IsTextFinished()
    {
        return textFinished;
    }

    /// <summary>
    /// Launch the exchanges of lines from the Dialog
    /// </summary>
    /// <param name="d"> The exchanges </param>
    /// <param name="textMesh"></param>
    public void LaunchDialog(Dialog dialog)
    {
        StartCoroutine(LaunchDialogDelayed(dialog));
    }

    /// <summary>
    /// Stop the dialog
    /// </summary>
    /// <param name="textMesh"></param>
    public void StopDialog()
    {
        StopAllCoroutines();
        textFinished = true;
        previousTextMesh.text = "";
    }

    /// <summary>
    /// Launch the next text
    /// </summary>
    /// <param name="textMesh"></param>
    public void NextText(TextMeshPro textMesh, string text)
    {
        if (textMesh != null)
            previousTextMesh.text = "";
        previousTextMesh = textMesh;
        letterCoroutine = StartCoroutine(DelayLetters(text, textMesh));
    }

    /// <summary>
    /// Pass text (used with shortcuts)
    /// </summary>
    /// <param name="textMesh"></param>
    private void PassText(TextMeshPro textMesh, string text)
    {
        if (letterCoroutine != null)
            StopCoroutine(letterCoroutine);
        textMesh.text = text;
        textFinished = true;
    }

    /// <summary>
    /// Launch the dialog coroutine
    /// </summary>
    /// <param name="dialog"></param>
    /// <returns></returns>
    private IEnumerator LaunchDialogDelayed(Dialog dialog)
    {
        textFinished = false;
        foreach (TextClone tc in dialog.exchanges)
        {
            TextMeshPro cloneTextMesh = GameManager.Instance.GetPlayerText(tc.id);
            // Get TextMesh of the player
            if (cloneTextMesh == null)
            {
                cloneTextMesh = GameManager.Instance.GetPlayerText(0);
                if(cloneTextMesh == null)
                {
                    Debug.LogError("There is no player or TextMesh in the scene ...");
                    yield break;
                }
            }
            // Set the text and the color to display
            string text = "<color=orange>" + tc.text + "</color>";
            if (colorPlayers.Length < tc.id)
                text = "<color=" + colorPlayers[tc.id].ToString() + ">" + tc.text + "</color>";
            // Show the text
            NextText(cloneTextMesh, text);
            yield return new WaitForSeconds((tc.text.Length * speedLetters) + tc.delaySwitch);
        }
        textFinished = true;
    }

    /// <summary>
    /// Delay apparation letters
    /// </summary>
    /// <param name="text"> the string to delayed </param>
    /// <returns></returns>
    private IEnumerator DelayLetters(string text, TextMeshPro textMesh)
    {
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
    }
}
