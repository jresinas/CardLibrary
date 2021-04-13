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

        foreach (Setup setup in setups) {
            if (setup.slot != null) {
                setup.slot.OnAdd += OnMove;
                //setup.slot.OnRemove += OnRemove;
                foreach (CardData cardData in setup.cards) {
                    if (cardData != null) {
                        Card card = cardData.Instantiate(setup.slot);
                        setup.slot.AddCard(0, card);
                    } else Debug.LogError("Card data could not be found");
                }
                //setup.slot.Sort();
            } else Debug.LogError("Slot could not be found");
        }
    }

    void OnMove(object slot, EventAction action) {
        switch (phase) {
            case (Phase.P1Draw):
                if (action.player == 1 && action.origin.name == "Deck" && action.destiny.name == "P1Hand") phase = Phase.P1Play;
                break;
        }
    }

    //void OnRemove(object slot, EventAction action) { }
}
