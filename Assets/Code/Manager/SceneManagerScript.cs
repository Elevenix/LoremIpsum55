using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject settingsPanel;

    [SerializeField]
    private Animator transition;

    private bool isTransition = false;
    private RandomSounds randomSounds;

    // Start is called before the first frame update
    void Start()
    {
        randomSounds = GetComponent<RandomSounds>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            OpenCloseSettings();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RestarttLevel();
        }
    }

    public void Quit()
    {
        randomSounds.PlaySound("Click");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    /// <summary>
    /// Load the next level
    /// </summary>
    public void NextLevel()
    {
        randomSounds.PlaySound("Click");
        StartCoroutine(TransitionLoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    /// <summary>
    /// Load the level given
    /// </summary>
    /// <param name="id"> the id in the buildsettings </param>
    public void LoadLevel(int id)
    {
        randomSounds.PlaySound("Click");
        StartCoroutine(TransitionLoadLevel(id));
    }

    /// <summary>
    /// Restart the current level
    /// </summary>
    public void RestarttLevel()
    {
        StartCoroutine(TransitionLoadLevel(SceneManager.GetActiveScene().buildIndex));
    }

    private IEnumerator TransitionLoadLevel(int id)
    {
        if (isTransition)
            yield break;
        isTransition = true;
        transition.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(id);
        isTransition = false;
    }

    /// <summary>
    /// Open and close the setting gameObject
    /// </summary>
    public void OpenCloseSettings()
    {
        randomSounds.PlaySound("Click");
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }
}
