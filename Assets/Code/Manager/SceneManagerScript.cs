using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagerScript : MonoBehaviour
{
    [SerializeField]
    private bool isMenu = false;

    [SerializeField]
    private GameObject settingsPanel;

    [SerializeField]
    private GameObject menuPanel;

    [SerializeField]
    private GameObject pausePanel;

    [SerializeField]
    private Button englishLanguage;

    [SerializeField]
    private Button frenchLanguage;

    [SerializeField]
    private Animator transition;

    [SerializeField]
    private float transitionDuration = 1;

    private bool isTransition = false;
    private RandomSounds randomSounds;

    // Start is called before the first frame update
    void Start()
    {
        transition.SetTrigger("Start");

        randomSounds = GetComponent<RandomSounds>();
        if (!isMenu)
        {
            menuPanel.SetActive(false);
        }
        else
        {
            menuPanel.SetActive(true);
        }
        pausePanel.SetActive(false);

        if(!PlayerPrefs.HasKey("Language"))
            ChangeLanguage(true);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            OpenClosePause();
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
        int idNextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        if (SceneManager.sceneCountInBuildSettings > idNextLevel)
            StartCoroutine(TransitionLoadLevel(idNextLevel));
    }

    public void BackToMenu()
    {
        randomSounds.PlaySound("Click");
        StartCoroutine(TransitionLoadLevel(0));
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
        yield return new WaitForSeconds(transitionDuration);
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
        if (!isMenu)
        {
            pausePanel.SetActive(!pausePanel.activeSelf);
        }
    }

    public void OpenClosePause()
    {
        if (!isMenu)
        {
            pausePanel.SetActive(!pausePanel.activeSelf);
        }
    }

    public void ChangeLanguage(bool isEn)
    {
        ColorBlock en = englishLanguage.colors;
        ColorBlock fr = frenchLanguage.colors;
        // white color block at normal color
        ColorBlock whiteLanguage = englishLanguage.colors;
        whiteLanguage.normalColor = Color.white;
        if (isEn)
        {
            en.normalColor = en.selectedColor;
            englishLanguage.colors = en;

            frenchLanguage.colors = whiteLanguage;

            PlayerPrefs.SetInt("Language", 0);
        }
        else
        {
            fr.normalColor = fr.selectedColor;
            frenchLanguage.colors = fr;

            englishLanguage.colors = whiteLanguage;

            PlayerPrefs.SetInt("Language", 1);
        }
    }
}
