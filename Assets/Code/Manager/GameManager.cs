using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private DialogManager dialogManager;
    [SerializeField] private SceneManagerScript sceneManager;
    [SerializeField] private bool hasGun = true;

    private List<GameObject> players = new List<GameObject>();
    private int actualGunHold = 0;

    public static GameManager Instance;

    private RandomSounds randomSounds;


    // Singleton
    public void Awake()
    {
        if(Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        randomSounds = GetComponent<RandomSounds>();
    }

    // Update is called once per frame
    void Update()
    {
        // Change gun with different players
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            ChangeGunHold();
            randomSounds.PlaySound("GunHolder");
        }
    }

    public void GiveGun()
    {
        hasGun = true;
        if (players[0].TryGetComponent(out PlayerTextMesh ptm))
        {
            ptm.textMeshId.text = "0";
        }

        if (players[0].TryGetComponent(out Gun gun))
        {
            gun.enabled = true;
        }
    }

    /// <summary>
    /// Add Player in the list
    /// </summary>
    /// <param name="player"></param>
    public void AddPlayer(GameObject player)
    {
        if (player.TryGetComponent(out PlayerTextMesh ptm) && hasGun)
        {
            ptm.textMeshId.text = (players.Count).ToString();
        }
        players.Add(player);

        if (player.TryGetComponent(out Gun gun))
        {
            if (players.Count == 1 && hasGun)
            {
                gun.enabled = true;
            }
            else
            {
                gun.enabled = false;
            }
        }
    }

    /// <summary>
    /// Launch the dialog given
    /// </summary>
    /// <param name="dialog"> Dialog to launch</param>
    public void LaunchDialog(Dialog dialog)
    {
        dialogManager.LaunchDialog(dialog);
    }

    /// <summary>
    /// Get the text meshPro of the clone
    /// </summary>
    /// <param name="id"> The id of the clone </param>
    /// <returns></returns>
    public TextMeshPro GetPlayerText(int id)
    {
        if (players[id].TryGetComponent(out PlayerTextMesh tmp))
            return tmp.textMeshDialog;
        return null;
    }

    /// <summary>
    /// Get the scene Manager
    /// </summary>
    /// <returns></returns>
    public SceneManagerScript GetSceneManager()
    {
        return sceneManager;
    }

    /// <summary>
    /// Change the gun of clones
    /// </summary>
    private void ChangeGunHold()
    {
        players[actualGunHold].GetComponent<Gun>().enabled = false;
        // change player
        actualGunHold++;
        if (actualGunHold >= players.Count)
            actualGunHold = 0;

        players[actualGunHold].GetComponent<Gun>().enabled = true;
    }
}
