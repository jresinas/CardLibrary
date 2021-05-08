using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Phase {
    P1Draw,
    P1Play,
    P1Discard,
    P2Draw,
    P2Play,
    P2Discard
};

public class GameManager : MonoBehaviour {
    [SerializeField] PlayerController[] players;
    public static Phase phase;

    void Awake() {
        phase = Phase.P1Draw;

        GameHelper.Deal(players);
    }

    public void OnMove(object slot, GameEventData data) {
        switch (phase) {
            case (Phase.P1Draw):
                //if (data.player == 1 && data.origin.name == "Deck" && data.destiny.name == "P1Hand") phase = Phase.P1Play;
                break;
        }
    }

    public void OnClickButton(ButtonController button) {
        Debug.Log(button.name);
    }


}
