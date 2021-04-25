//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Phase {
    public string name;
    public IPhase phase;

    public Phase(string name, IPhase phase) {
        this.name = name;
        //this.phase = phase; 
        this.phase = new P1Draw();
    }
}

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

public class Game : MonoBehaviour {
    public Phase[] phases;
    [SerializeField] protected InputCustom input;
    public PlayerController[] players;
    public static int BUTTONS = 2;
    public static Phase currentPhase;
    Dictionary<string, Phase> phasesDict = new Dictionary<string, Phase>();
    //public static Phase phase;
    //public static Game instance = null;

    protected void Awake() {
        //instance = this;
        GameObject[] slotObjs = GameObject.FindGameObjectsWithTag("Slot");
        foreach (GameObject slotObj in slotObjs) {
            Slot slot = slotObj.GetComponent<Slot>();
            //if (slot != null) slot.OnAdd += OnMove;
            if (slot != null && slot is Deck) ((Deck)slot).OnDraw += OnMove;
        }

        foreach (Phase phase in phases) {
            Debug.Log(phase);
            Debug.Log(phase.name);
            Debug.Log(phase.phase);

            phasesDict[phase.name] = new Phase(phase.name, phase.phase);
        }

        if (phases.Length > 0) currentPhase = phasesDict[phases[0].name];
        else Debug.LogError("No phases defined");

        if (input != null && currentPhase != null) {
            Debug.Log(currentPhase);
            Debug.Log(currentPhase.name);
            Debug.Log(currentPhase.phase);
            input.OnClick += currentPhase.phase.OnClick;
            input.OnClickUp += currentPhase.phase.OnClickUp;
            input.OnEnterHold += currentPhase.phase.OnEnterHold;
            input.OnHold += currentPhase.phase.OnHold;
            input.OnExitHold += currentPhase.phase.OnExitHold;
        }
    }

    protected void Deal() {
        foreach (PlayerController player in players) {
            foreach (Setup setup in player.setups) {
                if (setup.slot != null) {
                    //setup.slot.OnAdd += OnMove;
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
    }

    public void SetPhase(string phase) {
        currentPhase.phase.ExitPhase();
        currentPhase = phasesDict[phase];
        currentPhase.phase.EnterPhase();
    }


/*
    // Input events
    protected virtual void OnClick(object input, InputData data) {
        currentPhase.OnClick(input, data);
    }
    protected virtual void OnClickUp(object input, InputData data) { }
    protected virtual void OnEnterHold(object input, InputData data) { }
    protected virtual void OnHold(object input, InputData data) { }
    protected virtual void OnExitHold(object input, InputData data) { }
*/

    // Objects events
    protected virtual void OnMove(object slot, EventAction action) { }
    
}
