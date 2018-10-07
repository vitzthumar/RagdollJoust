using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

public class CustomNetworkManager : NetworkManager {

    public static CustomNetworkManager instance = null;

    private float nextRefreshTime;
    public InputField matchInputName;
    public Dropdown serverDropdown;

    private void Awake() {

        if (instance == null) {
            instance = this;
        }
 
        else if (instance != this) {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void StartHosting() {
        GameManager.instance.gameState = GameManager.GameState.Online;
        StartMatchMaker();

        if (matchInputName.text.Equals("")) {
            matchInputName.text = "Random Match";
        }

        matchMaker.CreateMatch(matchInputName.text, 2, true, "", "", "", 0, 0, OnMatchCreated);
    }


    private void OnMatchCreated(bool success, string extendedinfo, MatchInfo responsedata) {
        base.StartHost(responsedata);
        RefreshMatches();
    }

    private void Update() {
        if (Time.time >= nextRefreshTime) {
            RefreshMatches();
        }
    }

    private void RefreshMatches() {
        nextRefreshTime = Time.time + 5f;

        if (matchMaker == null)
            StartMatchMaker();

        matchMaker.ListMatches(0, 10, "", true, 0, 0, HandleListMatchesComplete);
    }


    private void HandleListMatchesComplete(bool success, string extendedinfo, List<MatchInfoSnapshot> responsedata) {
        AvailableMatchesList.HandleNewMatchList(responsedata);
    }

    public void JoinMatch(MatchInfoSnapshot match) {
        if (matchMaker == null)

            // inform the game manager that an online game is being played
            GameManager.instance.gameState = GameManager.GameState.Online;

            StartMatchMaker();

        matchMaker.JoinMatch(match.networkId, "", "", "", 0, 0, HandleJoinedMatch);
    }

    private void HandleJoinedMatch(bool success, string extendedinfo, MatchInfo responsedata) {
        StartClient(responsedata);
    }

    
    public void DestroyMatch() {
        StopMatchMaker();
        //Destroy(matchMaker);
        //DestroyMatch();
    }
    
    
}
