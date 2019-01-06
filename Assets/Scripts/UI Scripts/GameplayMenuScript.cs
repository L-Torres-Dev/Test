using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayMenuScript : MonoBehaviour {

    public GameObject spellsPanel;

    // Use this for initialization
    void Start() {
        gameObject.SetActive(false);
    }

    
    public void Active(bool active)
    {
        gameObject.SetActive(active);

        PauseOrUnpause();
        
    }

    public void PauseOrUnpause()
    {
        if (gameObject.activeSelf)
        {
            Time.timeScale = 0;
        }

        else
        {
            Time.timeScale = 1;
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void SpellsPanel()
    {
        spellsPanel.SetActive(true);
    }
}
