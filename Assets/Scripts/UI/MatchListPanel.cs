﻿using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.Networking.Match;

public class MatchListPanel : MonoBehaviour {
    [SerializeField] private JoinButton joinButtonPrefab;

    private void Awake() {
        AvailableMatchesList.OnAvailableMatchesChanged += AvailableMatchesList_OnAvailableMatchesChanged;
    }

    private void AvailableMatchesList_OnAvailableMatchesChanged(List<MatchInfoSnapshot> matches) {
        ClearExistingButtons();
        CreateNewJoinGameButtons(matches);
    }

    private void ClearExistingButtons() {
        if (this.gameObject != null) {
            var buttons = GetComponentsInChildren<JoinButton>();
            foreach (var button in buttons) {
             Destroy(button.gameObject);
            }
        }

    }

    private void CreateNewJoinGameButtons(List<MatchInfoSnapshot> matches) {
        foreach (var match in matches) {
            var button = Instantiate(joinButtonPrefab);
            button.Initialize(match, transform);
        }
    }
}