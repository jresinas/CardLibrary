//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventData {
    public int player;
    public Card card;
    public Slot origin;
    public Slot destiny;

    public GameEventData(int player, Card card, Slot origin, Slot destiny) {
        this.player = player;
        this.card = card;
        this.origin = origin;
        this.destiny = destiny;
    }
}

public class GameHandler : MonoBehaviour {
    public GameManager manager;

    protected void Awake() {
        GameObject[] slotObjs = GameObject.FindGameObjectsWithTag("Slot");
        foreach (GameObject slotObj in slotObjs) {
            Slot slot = slotObj.GetComponent<Slot>();
            //if (slot != null) slot.OnMove += OnMove;
            if (slot != null && slot is Deck) ((Deck)slot).OnDraw += manager.OnMove;
        }
    }

}
