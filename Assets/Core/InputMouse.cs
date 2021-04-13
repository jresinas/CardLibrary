using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMouse : MonoBehaviour {
    protected float DRAG_HEIGHT = 1f;
    protected float PRESS_THRESHOLD = 0.15f;

    protected int state = 0;
    protected bool[] hold = new bool[GameManager.BUTTONS];
    protected float[] press = new float[GameManager.BUTTONS];
    protected bool[] drag = new bool[GameManager.BUTTONS];
    protected CardController[] selectedCard = new CardController[GameManager.BUTTONS];

    protected virtual void ActionClick(int button) { }
    protected virtual void EnterActionHold(int button) { }
    protected virtual void ExitActionHold(int button) { }
    protected virtual void ChangeState(int current, int next) { }

    void Update() {
        if (state == 0) {
            ButtonBehaviour(0);
            ButtonBehaviour(1);
        } else {
            for (int i = 0; i < GameManager.BUTTONS; i++) {
                if (Input.GetButtonDown("Button" + i)) {
                    for (int j = 0; j < GameManager.BUTTONS; j++) {
                        ExitZoom(j);
                    }
                }
            }
            /*
            if (Input.GetButtonDown("Button0") || Input.GetButtonDown("Button1")) {
                ExitZoom(0);
                ExitZoom(1);
            }
            */
        }

        //if (drag && card != null) Debug.Log(CheckDestiny());
    }

    /*
    // Return true if dragging and card can be dropped in current slot below cursor
    bool CheckDestiny() {
        if (drag && card != null) {
            Slot slot = GetSlot();
            return (slot != null && slot.AllowAdd(card));
        } else return false;
    }
    */

    void ButtonBehaviour(int button) {
        if (Input.GetButtonDown("Button" + button)) {
            selectedCard[button] = GetCard();
        }

        if (Input.GetButtonUp("Button" + button)) {
            CardController card = GetCard();
            if (hold[button]) {
                hold[button] = false;
                ExitActionHold(button);
            } else if (card != null && card == selectedCard[button]) ActionClick(button);
        }

        if (Input.GetButton("Button" + button)) {
            CardController card = GetCard();
            if (card != null && card == selectedCard[button]) press[button] += Time.deltaTime;
            else press[button] = 0;
        } else {
            press[button] = 0;
        }

        if (press[button] > PRESS_THRESHOLD) {
            EnterActionHold(button);
            hold[button] = true;
        }

        if (drag[button]) {
            if (selectedCard[button] != null) {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Vector3 point = ray.GetPoint(5f);
                selectedCard[button].gameObject.transform.position = new Vector3(point.x, DRAG_HEIGHT, point.z);
            }
        }
    }

    void SetState(int state) {
        ChangeState(this.state, state);
        this.state = state;
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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray);
        foreach (RaycastHit hit in hits) {
            if (hit.collider.tag == "Slot") {
                return hit.collider.GetComponentInParent<Slot>();
            }
        }
        return null;
    }

    protected void EnterZoom(int button) {
        if (selectedCard[button] != null) {
            selectedCard[button].EnterZoom();
            SetState(1);
        }
    }

    protected void EnterZoomReveal(int button) {
        if (selectedCard[button] != null) {
            selectedCard[button].EnterZoomReveal(GameManager.player);
            SetState(1);
        }
    }

    protected void ExitZoom(int button) {
        if (selectedCard[button] != null) {
            selectedCard[button].ExitZoom();
            selectedCard[button] = null;
            SetState(0);
        }
    }

    protected void EnterDrag(int button) {
        if (selectedCard[button] != null) drag[button] = true;
    }

    protected void ExitDrag(int button) {
        if (selectedCard[button] != null) {
            Slot slot = GetSlot();
            selectedCard[button].Move(GameManager.player, slot);
            selectedCard[button].ExitDrag();
            selectedCard[button] = null;
            drag[button] = false;
        }
    }

    protected void Flip(int button) {
        if (selectedCard[button] != null) {
            selectedCard[button].Flip(GameManager.player);
        }
    }
}
