using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCustom : InputMouse {
    /*
    public event EventHandler<InputData> OnClick;
    public event EventHandler<InputData> OnEnterHold;
    public event EventHandler<InputData> OnHold;
    public event EventHandler<InputData> OnExitHold;
    */

    /*
    protected override void ActionClick(int button, Card card, Slot slot) {
        switch (button) {
            case 0:
                //Flip(button);
                //Custom2(button);
                Custom3(button, card, slot);
                break;
            case 1:
                EnterZoom(button);
                break;
        }
    }

    protected override void EnterActionHold(int button, Card card) {
        switch (button) {
            case 0:
                EnterDrag(button);
                break;
            case 1:
                EnterZoomReveal(button);
                break;
        }
    }

    protected override void ExitActionHold(int button, Card card, Slot slot) {
        switch (button) {
            case 0:
                Target response = ExitDrag(button);
                MoveCard(response);
                break;
            case 1:
                // Never is called because EnterZoom and EnterZoomReveal change InputMouse state
                break;
        }
    }
    */

    public void EnterZoom(int button) {
        if (selectedCard[button] != null) {
            selectedCard[button].EnterZoom();
            SetState(1);
        }
    }

    public void EnterZoomReveal(int button) {
        if (selectedCard[button] != null) {
            selectedCard[button].EnterZoomReveal(UserManager.player);
            SetState(1);
        }
    }

    public void ExitZoom(int button) {
        Debug.Log("ExitZoom" + button);
        if (selectedCard[button] != null) {
            Debug.Log("ExitZoom" + button+" Enter");
            selectedCard[button].ExitZoom();
            selectedCard[button] = null;
            SetState(0);
        }
    }

    public void ExitZoom() {
        for (int i = 0; i < GameManager.BUTTONS; i++) ExitZoom(i);
    }

    public void EnterDrag(int button) {
        if (selectedCard[button] != null) drag[button] = true;
    }

    public Target ExitDrag(int button) {
        if (selectedCard[button] != null) {
            Card card = selectedCard[button];
            Slot targetSlot = GetTarget<Slot>();
            Card targetCard = GetTarget<Card>();
            //selectedCard[button].ExitDrag();
            selectedCard[button] = null;
            drag[button] = false;
            return new Target(card, targetCard, targetSlot);
        } else return null;
    }

    public void Flip(int button) {
        if (selectedCard[button] != null) {
            selectedCard[button].Flip(UserManager.player);
        }
    }

    public void SetState(int state) {
        ChangeState(this.state, state);
        this.state = state;
    }

    public int GetState() {
        return state;
    }




    /* Example */
    public Slot customSlotOrigin;
    public Slot customSlotDestiny;
    public void Custom1(int button) {
        if (selectedCard[button] != null) {
            Slot currentSlot = selectedCard[button].GetSlot();

            //if (currentSlot == customSlotOrigin) selectedCard[button].Move(UserManager.player, customSlotDestiny);
            if (currentSlot == customSlotOrigin) currentSlot.Move(UserManager.player, selectedCard[button], customSlotDestiny);
            else Flip(button);
        }
    }

    public void Custom2(int button) {
        if (selectedCard[button] != null) {
            Slot currentSlot = selectedCard[button].GetSlot();

            if (currentSlot == customSlotOrigin) {
                //selectedCard[button].Flip(UserManager.player);
                //selectedCard[button].Move(UserManager.player, customSlotDestiny);
                currentSlot.Move(UserManager.player, selectedCard[button], customSlotDestiny);
                Flip(button);
            }
        }
    }

    public void Custom3(int button, Card card, Slot slot) {
        if (card != null && slot == customSlotOrigin) {
            //card.Move(UserManager.player, customSlotDestiny);
            //((CardController)card).Flip(UserManager.player);
            ((Deck)slot).Draw(UserManager.player, customSlotDestiny);
        }
    }




    public void MoveCard(Target targets) {
        Slot origin = targets.card.GetSlot();
        if (targets.targetSlot != null && origin != null) {
            origin.Move(UserManager.player, targets.card, targets.targetSlot);
        } 
        ((CardController)targets.card).ExitDrag();
    }
}
