//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public static class InputHelper {// : MonoBehaviour {
    static float DRAG_HEIGHT = 1f;
    static CardController zoom;

    public static void EnterZoom(CardController card) {
        if (card != null) {
            zoom = card;
            zoom.EnterZoom();
        }
    }

    public static void EnterZoomReveal(PlayerController player, CardController card) {
        if (card != null) {
            zoom = card;
            zoom.EnterZoomReveal(player.player);
        }
    }

    public static void ExitZoom() {
        if (zoom != null) {
            zoom.ExitZoom();
            // To avoid enter zoom when click to exit zoom
            //input.ClearSelectedCards();
            zoom = null;
        }
    }

    public static void Drag(CardController card) {
        if (card != null) {
            float cameraHeight = Camera.main.transform.position.y;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 point = ray.GetPoint(cameraHeight);
            card.transform.position = new Vector3(point.x, DRAG_HEIGHT, point.z);
        }
    }

    public static void Drop(PlayerController player, CardController card, Slot targetSlot) {
        if (card != null) {
            Slot origin = card.GetSlot();
            if (targetSlot != null && origin != null) origin.Move(player.player, card, targetSlot);
            card.ExitDrag();
        }
    }


    public static void Flip(PlayerController player, CardController card) {
        if (card != null) {
            card.Flip(player.player);
        }
    }

    /// Example 
    public static void Draw(PlayerController player, Slot slotDestiny, Deck deck, Board hand) {
        if (slotDestiny == deck) {
            deck.Draw(player.player, hand);
        }
    }

    public static bool IsZoom() {
        return zoom;
    }

    /*
    public void EnterZoom(CardController card) {
        if (card != null) {
            zoom = card;
            zoom.EnterZoom();
        }
    }

    public void EnterZoomReveal(CardController card) {
        if (card != null) {
            zoom = card;
            zoom.EnterZoomReveal(player.player);
        }
    }

    public void ExitZoom() {
        if (zoom != null) {
            zoom.ExitZoom();
            // To avoid enter zoom when click to exit zoom
            //input.ClearSelectedCards();
            zoom = null;
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
            if (targetSlot != null && origin != null) origin.Move(player.player, card, targetSlot);
            card.ExitDrag();
        }
    }


    public void Flip(CardController card) {
        if (card != null) {
            card.Flip(player.player);
        }
    }

    /// Example 
    public void Draw(Slot slotDestiny, Deck deck, Board hand) {
        if (slotDestiny == deck) {
            deck.Draw(player.player, hand);
        }
    }

    public bool IsZoom() {
        return zoom;
    }
    */
}
