using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMouse : MonoBehaviour {
    // Time pressing mouse button to start dragging
    float DRAG_THRESHOLD = 0.15f;
    // Card height when is dragging
    float DRAG_HEIGHT = 1f;

    // Current selected card (for dragging or zoom)
    CardController card = null;
    // Is zoom a card
    bool zoom = false;
    // Is dragging a card
    bool drag = false;
    // Time mouse button is pressed
    float press = 0;

    void Update() {
        // Select card
        if (Input.GetButtonDown("Fire1")) {
            card = GetCard();
        }

        // Left mouse button behaviour
        if (card != null && Input.GetButtonUp("Fire1")) {
            if (drag) ExitDrag();
            else {
                if (zoom) ExitZoom();
                else EnterZoom();
            }
        }

        // Holding left mouse button 
        if (Input.GetButton("Fire1")) {
            if (!zoom && card != null) {
                CardController selectedCard = GetCard();
                if (selectedCard == card) press += Time.deltaTime;
                else press = 0;
            }
        } else {
            press = 0;
        }

        // Start dragging
        if (!drag && press > DRAG_THRESHOLD) {
            EnterDrag();
        }

        // Dragging behaviour
        if (drag) {
            if (card != null) {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Vector3 point = ray.GetPoint(5f);
                card.gameObject.transform.position = new Vector3(point.x, DRAG_HEIGHT, point.z);
            }
        }

        // Right mouse button behaviour
        if (Input.GetButtonUp("Fire2") && !zoom) {
            CardController revealCard = GetCard();
            if (revealCard != null) {
                revealCard.Flip();
            }
        }

        //if (drag && card != null) Debug.Log(CheckDestiny());
    }

    // Return true if dragging and card can be dropped in current slot below cursor
    bool CheckDestiny() {
        if (drag && card != null) {
            Slot slot = GetSlot();
            return (slot != null && slot.AllowAdd(card));
        } else return false;
    }

    // Get current card below cursor
    CardController GetCard() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            if (hit.collider.tag == "Card") {
                return hit.collider.GetComponentInParent<CardController>();
            }
        }
        return null;
    }

    // Get current slot below cursor
    Slot GetSlot() {
        //Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray);
        foreach (RaycastHit hit in hits) {
            if (hit.collider.tag == "Slot") {
                return hit.collider.GetComponentInParent<Slot>();
            }
        }
        return null;
    }

    void EnterZoom() {
        Card currentCard = GetCard();
        if (card != null && card == currentCard) {
            card.EnterZoom();
            //card.EnterZoomReveal();
            zoom = true;
        }
    }

    void ExitZoom() {
        if (card != null) {
            card.ExitZoom();
            card = null;
            zoom = false;
        }
    }

    void EnterDrag() {
        if (card != null) drag = true;
    }

    void ExitDrag() {
        if (card != null) {
            Slot slot = GetSlot();
            /*
            if (slot != null && slot.AllowDrag(card)) {
                Slot currentSlot = card.GetSlot();
                if (currentSlot.AllowAdd(card) || (slot == currentSlot && currentSlot.AllowReorder(card))) {
                    if (!currentSlot.Move(slot, card)) card.ExitDrag();
                }
            } else card.ExitDrag();
            */
            card.Move(slot);
            card.ExitDrag();
            card = null;
            drag = false;
        }
    }
}
