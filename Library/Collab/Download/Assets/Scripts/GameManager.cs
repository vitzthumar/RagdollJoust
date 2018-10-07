using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    public bool gameCanRun = false;
    public bool paused = false;
    public float duration = 5;
    public bool countDownDone = false;
    public GameObject countDownImage;
    
    public GameObject winCanvasHandler;

    public GameObject PlayerPrefab;
    // class for players that will be preserved across rounds but not games
    class Jouster {
        public int playerNumber;
        public bool isAlive;
        public int playerWins;
        Color playerColor;

        // other fun statistics/things that might be cool to keep track off
        string playerName;
        int numberOfLanceHits;
        int totalDistanceTraveled;
        int totalTimeSurvived;

        // constructor
        public Jouster(int number) {
            playerNumber = number;
            playerWins = 0;
        }
    }

    List<Jouster> players;
    // the number of wins needed to win the game
    int winsNeeded = 1;
    int numberOnlinePlayers = 0;

    public enum GameState {
        MainMenu,
        SplitScreen,
        Online
    }
    public GameState gameState;

    // Use this for initialization
    void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);

        }
        DontDestroyOnLoad(this);

        players = new List<Jouster>();

        // load the main menu
        gameState = GameState.MainMenu;

        
    }

    // Begin a split-screen game with 2 players
    public void StartSplitScreenGame() {
        //winCanvasHandler = GameObject.Find("WinCanvas");
        countDownImage = GameObject.Find("CountdownCanvas");
        //winCanvasHandler = GameObject.Find("WinCanvas");
        gameState = GameState.SplitScreen;

        // instantiate the first player's character
        InstantiatePlayer(0, true);

        // instantiate the second player's character
        InstantiatePlayer(1, true);

       // StartCoroutine(CountdownTimer(3));
    }


    public void Update() {
        if (gameState == GameState.SplitScreen && countDownDone == true && countDownImage != null) {
            countDownImage.SetActive(false);
            gameCanRun = true;
        }
    }
    void StartOnlineGame() {
        // with the provided number of players, start an online game and set the state of GameState to online
        gameState = GameState.Online;        
    }

    // Used for instantiating players at the beginning of a game
    public void InstantiatePlayer(int playerNumber, bool isNewGame) {

        GameObject currentSpawn = GameObject.Find("SpawnPoint0" + playerNumber);
        GameObject currentPlayer = Instantiate(PlayerPrefab, currentSpawn.transform.position, currentSpawn.transform.rotation);
        //GameObject currentPlayer = Instantiate(PlayerPrefab, Vector3.zero, Quaternion.identity);

        // access the player camera attached to the player as the first game object
        currentPlayer.transform.GetChild(0).GetComponent<CameraController>().InitializeCamera(playerNumber);

        // match the player number to the game manager's player number
        currentPlayer.GetComponent<SplitScreenPlayer>().playerNumber = playerNumber;

        // only execute if a new game has been started, NOT a new round
        if (isNewGame) {
            
            // add this player to a list of all the players which exist in the current game
            players.Add(new Jouster(playerNumber));
            players[playerNumber].isAlive = true;
        }
    }

    // Used for adding instantiated players to an online game
    public void AddOnlinePlayer() {
        numberOnlinePlayers++;
        players.Add(new Jouster(numberOnlinePlayers - 1));
        players[numberOnlinePlayers - 1].isAlive = true;
    }


    // Called when a player touches the ground
    public void PlayerDied(int playerNumber) {

        // set the player to dead
        players[playerNumber].isAlive = false;
        
        // create a list of all of the players who have not died
        List<int> alivePlayers = new List<int>();

        // check through all the players to see if the round has ended
        for (int count = 0; count < players.Count; count++) {
            
            // if the player is alive, keep track of their player number
            if (players[count].isAlive) {
                alivePlayers.Add(count);
            }
        }

        // if there is only one player left, then the round is over
        if (alivePlayers.Count == 1) {
            // give the winner a win
            players[alivePlayers[0]].playerWins++;
            // end the round and pass the winner on
            EndRound(players[alivePlayers[0]]);
        }
    }

    // Called when only one player is alive
    void EndRound(Jouster winner) {

        
        // destroy all players
        GameObject[] existingPlayers = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject existingPlayer in existingPlayers) {
            Destroy(existingPlayer.gameObject);
        }



        if (gameState == GameState.SplitScreen) {
            // check if the winner's number of wins is equal to the number of wins needed
            if (winner.playerWins == winsNeeded) {
                // the winner of this round won the game
                // display the win screen
                // display the stats
                // return to the main menu
                // clear the list of players
                players.Clear();

                // reset the number of online players to 0
                //numberOnlinePlayers = 0;
                Debug.Log("GAME OVER");
            } else {
                // start a new round
                StartNewRound();
            }
        }

        if (gameState == GameState.Online) {
            // display the name of the player who won
            Debug.Log(winner.playerNumber);
            numberOnlinePlayers = 0;
            // clear the list of active players
            players.Clear();
            // repause the game
            NetworkInformer.instance.pauseGame = true;           

        }
        
        // on click, return to the main menu


        // return to the menu state
        gameCanRun = false;
        gameState = GameState.MainMenu;


        if (winner.playerNumber == 0) {
            SceneManager.LoadScene("RedWin");
        } else {
            SceneManager.LoadScene("BlueWin");
        }
        //SceneManager.LoadScene("Menu");
        FindObjectOfType<CustomNetworkManager>().DestroyMatch();

    }

    void StartNewRound() {
        
        // re-instantiate all of the players from the last round
        // each player's number corresponds to their previous index in the list of players
        for (int count = 0; count < players.Count; count++) {
            // instantiate the player and set their status to alive
             InstantiatePlayer(count, false);
            players[count].isAlive = true;            
        }

        // do some kind of countdown
        // then allow players to move

    }

    public void StartCountdown(float duration) {
        StartCoroutine(CountdownTimer(duration));
    }

    IEnumerator CountdownTimer(float givenDuration) {

        duration = givenDuration;

        while (duration > 0) {
            duration -= Time.deltaTime;

            yield return null;
        }

        if (gameState == GameState.SplitScreen) {
            gameCanRun = true;
        }

        if (gameState == GameState.Online) {
            NetworkInformer.instance.pauseGame = false;
        }
    }


}
