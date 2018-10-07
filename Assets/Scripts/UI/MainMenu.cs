using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {


    public void QuitGame() {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public void StartOffline1() {
        GameManager.instance.gameState = GameManager.GameState.SplitScreen;
        SceneManager.LoadScene("Level1");
    }

    public void StartOffline2() {
        GameManager.instance.gameState = GameManager.GameState.SplitScreen;
        SceneManager.LoadScene("Level2");
    }

    public void StartOffline3() {
        GameManager.instance.gameState = GameManager.GameState.SplitScreen;
        SceneManager.LoadScene("Level3");
    }

    public void LoadMain() {
        SceneManager.LoadScene("Menu");

    }
}
