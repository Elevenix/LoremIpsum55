using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<GameObject> players = new List<GameObject>();
    private int actualGunHold = 0;

    public static GameManager Instance;

    // Singleton
    public void Awake()
    {
        if(Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Change gun with different players
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            ChangeGunHold();
        }
    }

    /// <summary>
    /// Add Player in the list
    /// </summary>
    /// <param name="player"></param>
    public void AddPlayer(GameObject player)
    {
        players.Add(player);
        if(player.TryGetComponent(out Gun gun))
        {
            if (players.Count == 1)
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
