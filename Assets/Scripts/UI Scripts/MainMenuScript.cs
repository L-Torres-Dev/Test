using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
    public void StartGame()
    {
        //Load new game Scene
        SceneManager.LoadScene(SceneNames.HISTORY_REMNANT_ROOM);

        Time.timeScale = 1;
    }

    public void LoadGameplay()
    {
        SceneManager.LoadScene(SceneNames.GAMEPLAY_SCENE);
        Time.timeScale = 1;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
