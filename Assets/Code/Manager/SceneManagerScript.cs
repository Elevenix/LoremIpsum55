using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject settingsPanel;

    [SerializeField]
    private GameObject quitButton;

    [SerializeField]
    private GameObject menuButton;

    [SerializeField]
    private GameObject playButton;

    [SerializeField]
    private GameObject menuPanel;

    [SerializeField]
    private Animator transition;

    private bool isTransition = false;
    private RandomSounds randomSounds;

    // Start is called before the first frame update
    void Start()
    {
        randomSounds = GetComponent<RandomSounds>();
        if (SceneManager.GetActiveScene().name != "Menu")
        {
            menuPanel.SetActive(false);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            OpenCloseMenu();
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

    public void BackToMenu()
    {
        randomSounds.PlaySound("Click");
        SceneManager.LoadScene("Menu");
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
        if (SceneManager.GetActiveScene().name != "Menu")
        {
            menuButton.SetActive(!menuButton.activeSelf);
        }
    }

    public void OpenCloseMenu()
    {
        if (SceneManager.GetActiveScene().name != "Menu")
        {
            menuPanel.SetActive(!menuPanel.activeSelf);
            menuButton.SetActive(!menuButton.activeSelf);
            quitButton.SetActive(!quitButton.activeSelf);
            playButton.SetActive(!playButton.activeSelf);
        }
    }

}
