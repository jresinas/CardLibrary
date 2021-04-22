using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCustom : InputMouse {
    protected float DRAG_HEIGHT = 1f;
    CardController zoom;

    public void EnterZoom(CardController card) {
        if (card != null) {
            zoom = card;
            zoom.EnterZoom();
            SetState(1);
        }
    }

    public void EnterZoomReveal(CardController card) {
        if (card != null) {
            zoom = card;
            zoom.EnterZoomReveal(UserManager.player);
            SetState(1);
        }
    }

    public void ExitZoom() {
        if (zoom != null) {
            zoom.ExitZoom();
            // To avoid enter zoom when click to exit zoom
            for (int i = 0; i < GameManager.BUTTONS; i++) selectedCard[i] = null;
            zoom = null;
            SetState(0);
        }
    }

    public void Drag(CardController card) {
        if (card != null) {
            float cameraHeight = Camera.main.transform.position.y;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 point = ray.GetPoint(cameraHeight);
            card.transform.position = new Vector3(point.x, DRAG_HEIGHT, point.z);
        }
    }

    public void Drop(CardController card, Slot targetSlot) {
        if (card != null) {
            Slot origin = card.GetSlot();
            if (targetSlot != null && origin != null) origin.Move(UserManager.player, card, targetSlot);
            card.ExitDrag();
        }
    }


    public void Flip(CardController card) {
        if (card != null) {
            card.Flip(UserManager.player);
        }
    }


    public void SetState(int state) {
        ChangeState(this.state, state);
        this.state = state;
    }

    public int GetState() {
        return state;
    }



    /// Example 
    public void Draw(Slot slotDestiny, Deck deck, Board hand) {
        if (slotDestiny == deck) {
            deck.Draw(UserManager.player, hand);
        }
    }

}
