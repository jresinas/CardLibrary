using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCustom : InputMouse {
    protected float DRAG_HEIGHT = 1f;
    CardController zoom;
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
            zoom = selectedCard[button];
            zoom.EnterZoom();
            SetState(1);
        }
    }

    public void EnterZoomReveal(int button) {
        if (selectedCard[button] != null) {
            zoom = selectedCard[button];
            zoom.EnterZoomReveal(UserManager.player);
            SetState(1);
        }
    }

    /*
    public void ExitZoom(int button) {
        if (zoom != null) {
            zoom.ExitZoom();
            selectedCard[button] = null;
            zoom = null;
            SetState(0);
        }
    }
    */

   
    public void ExitZoom() {
        //for (int i = 0; i < GameManager.BUTTONS; i++) ExitZoom(i);
        if (zoom != null) {
            zoom.ExitZoom();
            for (int i = 0; i < GameManager.BUTTONS; i++) selectedCard[i] = null;
            zoom = null;
            SetState(0);
        }
    }

    public void Drag(int button) {
        if (selectedCard[button] != null) {
            float cameraHeight = Camera.main.transform.position.y;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 point = ray.GetPoint(cameraHeight);
            selectedCard[button].gameObject.transform.position = new Vector3(point.x, DRAG_HEIGHT, point.z);
        }
    }

    public void Drop(Card card, Slot targetSlot) {
        Slot origin = card.GetSlot();
        if (targetSlot != null && origin != null) {
            origin.Move(UserManager.player, card, targetSlot);
        }
        ((CardController)card).ExitDrag();
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

}
