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
    public Deck playerDeck;
    public Board playerHand;


    void Awake() {
        base.Awake();
        phase = Phase.P1Draw;


        InputCustom ic = GetComponent<InputCustom>();
        ic.OnClick += OnClick;

        Deal();
    }

    protected override void OnMove(object slot, EventAction action) {
        switch (phase) {
            case (Phase.P1Draw):
                if (action.player == 1 && action.origin.name == "Deck" && action.destiny.name == "P1Hand") phase = Phase.P1Play;
                break;
        }
    }





    protected override void OnClick(object source, InputData data) {
        if (input.GetState() != 0) {
            Debug.Log("OnClick");
            input.ExitZoom();
        }
    }

    protected override void OnClickUp(object source, InputData data) {
        if (input.GetState() == 0) {
            switch (data.button) {
                case 0:
                    //Flip(button);
                    //Custom2(button);
                    input.Draw(data.targetSlot, playerDeck, playerHand);
                    break;
                case 1:
                    //input.EnterZoom(data.button);
                    input.EnterZoom(data.selectedCard);
                    break;
            }
        }
    }

    protected override void OnEnterHold(object source, InputData data) {
        switch (data.button) {
            case 0:
                Debug.Log("OnEnterHold0");
                //input.EnterDrag(data.button);
                break;
            case 1:
                Debug.Log("OnEnterHold1");
                //input.EnterZoomReveal(data.button);
                input.EnterZoomReveal(data.selectedCard);
                break;
        }
    }

    protected override void OnExitHold(object source, InputData data) {
        switch (data.button) {
            case 0:
                input.Drop(data.selectedCard, data.targetSlot);
                break;
            case 1:
                // Never is called because EnterZoom and EnterZoomReveal change InputMouse state
                break;
        }
    }

    protected override void OnHold(object source, InputData data) {
        switch (data.button) {
            case 0:
                //input.Drag(data.button);
                input.Drag(data.selectedCard);
                break;
        }
    }
}
