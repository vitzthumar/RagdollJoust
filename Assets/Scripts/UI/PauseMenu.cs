using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	public static bool GamePaused = false;
	public GameObject pauseUI;

    private void Awake() {
        pauseUI.SetActive(false);    
    }

    // Update is called once per frame
    void Update () {
		if (Input.GetKeyDown (KeyCode.Escape) && (GameManager.instance.gameState == GameManager.GameState.SplitScreen)) 
		{
			if (GamePaused) {
				Resume ();
			} else 
			{
				Pause ();
			}

		}
			
	}

	public void Resume(){
		pauseUI.SetActive(false);
		Time.timeScale = 1f;
		GamePaused = false;
	}
	void Pause(){
		pauseUI.SetActive(true);
		Time.timeScale = 0f;
		GamePaused = true;
	}

    public void ReturnToMain() {
        SceneManager.LoadScene("Menu");
        GameManager.instance.gameState = GameManager.GameState.MainMenu;
        GameManager.instance.gameCanRun = false;

    }

    public void Quit() {
        Application.Quit();
    }
}
