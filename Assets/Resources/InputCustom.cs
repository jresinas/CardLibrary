using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCustom : InputMouse {
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

    /* Example */
    public Slot customSlotOrigin;
    public Slot customSlotDestiny;
    void Custom1(int button) {
        if (selectedCard[button] != null) {
            Slot currentSlot = selectedCard[button].GetSlot();

            //if (currentSlot == customSlotOrigin) selectedCard[button].Move(UserManager.player, customSlotDestiny);
            if (currentSlot == customSlotOrigin) currentSlot.Move(UserManager.player, selectedCard[button], customSlotDestiny);
            else Flip(button);
        }
    }

    void Custom2(int button) {
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

    void Custom3(int button, Card card, Slot slot) {
        if (card != null && slot == customSlotOrigin) {
            //card.Move(UserManager.player, customSlotDestiny);
            //((CardController)card).Flip(UserManager.player);
            ((Deck)slot).Draw(UserManager.player, customSlotDestiny);
        }
    }




    void MoveCard(Target targets) {
        Slot origin = targets.card.GetSlot();
        if (targets.targetSlot != null && origin != null) {
            origin.Move(UserManager.player, targets.card, targets.targetSlot);
        } 
        ((CardController)targets.card).ExitDrag();
    }
}
