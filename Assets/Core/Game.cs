//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventAction {
    public int player;
    public Card card;
    public Slot origin;
    public Slot destiny;

    public EventAction(int player, Card card, Slot origin, Slot destiny) {
        this.player = player;
        this.card = card;
        this.origin = origin;
        this.destiny = destiny;
    }
}

[System.Serializable]
public class Setup {
    public Slot slot;
    public CardData[] cards;
}
public class Game : MonoBehaviour {
    public static int BUTTONS = 2;
    public static Phase phase;
    public Setup[] setups;
    //public static Game instance = null;

    protected void Awake() {
        //instance = this;
    }

    /*
    public void OnAdd(int player, Card card, Slot slot) {

    }
    */
}
