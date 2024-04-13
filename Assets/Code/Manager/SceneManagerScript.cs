using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{

    //Add scenes in inspector
    [SerializeField]
    private List<Scene> levelSceneListe;

    [SerializeField]
    private AudioSource m_AudioSource;
    [SerializeField]
    private AudioSource m_MusicSource;
    public static float m_SfxVolume = 1.0f;

    public static float m_MusicVolume = 1.0f;

    [SerializeField]
    private GameObject settingsPanel;

    [SerializeField]
    private Animator transition;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadGame()
    {
        SceneManager.LoadScene("MovementScene");
    }

    public void Quit()
    {
        // Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void NextLevel()
    {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        transition.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        transition.SetTrigger("Start");
    }

    public void ChangeVolume(float newValue)
    {
        m_SfxVolume = newValue;
        m_AudioSource.volume = m_SfxVolume;
    }

    public void ChangeMusicVolume(float newValue)
    {
        m_MusicVolume = newValue;
        m_MusicSource.volume = m_MusicVolume;

    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }
}
