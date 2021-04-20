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
public class GameManager : Game {
    void Awake() {
        base.Awake();
        phase = Phase.P1Draw;

        Deal();
    }

    protected override void OnMove(object slot, EventAction action) {
        switch (phase) {
            case (Phase.P1Draw):
                if (action.player == 1 && action.origin.name == "Deck" && action.destiny.name == "P1Hand") phase = Phase.P1Play;
                break;
        }
    }

}
